using System;
using System.Collections.Generic;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;

namespace DeadDog.PDF
{
    public partial class PDFDocument
    {
        private class Writer
        {
            private Document document;
            private PdfWriter writer;
            private PdfContentByte cb;

            private PageSize currentsize = PageSize.A4;
            private bool firstpage = true; //Used for determining that the first Page object does not create a blank page.

            public Writer(Document document, string filename, IPDFPage[] pages)
            {
                this.document = document;
                try
                {
                    writer = PdfWriter.GetInstance(document, new System.IO.FileStream(filename, System.IO.FileMode.Create));
                    document.Open();
                    cb = writer.DirectContent;

                    foreach (IPDFPage page in pages)
                        draw(page);
                }
                catch (DocumentException de)
                {
                    Console.Error.WriteLine(de.Message);
                }
                catch (System.IO.IOException ioe)
                {
                    Console.Error.WriteLine(ioe.Message);
                }
                document.Close();
            }

            private int textAlignment(TextAlignment ta)
            {
                switch (ta)
                {
                    case TextAlignment.Left: return 0;
                    case TextAlignment.Center: return 1;
                    default: return 2;
                }
            }

            private void draw(IPDFPage page)
            {
                currentsize = page.PageSize;
                if (!firstpage)
                    document.NewPage();
                firstpage = false;

                ObjectCollector collector = new ObjectCollector();
                page.Collect(collector);

                foreach (IPDFObject obj in collector.GetObjects)
                    draw(obj);
            }

            private void draw(IPDFObject obj)
            {
                System.Drawing.PointF point = obj.Handler.Location;
                IPDFGroup par = obj.Handler.parent;
                obj.Handler.parent = null;
                System.Drawing.PointF point2 = obj.Handler.Location;
                obj.Handler.Location = point;

                ObjectCollector collector = new ObjectCollector();
                obj.Collect(collector);

                foreach (IPDFObject p in collector.GetObjects)
                {
                    if (p is DeadDog.PDF.TextLine)
                        drawText(p as DeadDog.PDF.TextLine);
                    else if (p is DeadDog.PDF.Box)
                        drawBox(p as DeadDog.PDF.Box);
                    else if (p is DeadDog.PDF.Elipse)
                        drawElipse(p as DeadDog.PDF.Elipse);
                    else if (p is DeadDog.PDF.Line)
                        drawLine(p as DeadDog.PDF.Line);
                    else if (p is DeadDog.PDF.ImageObject)
                        drawImage(p as DeadDog.PDF.ImageObject);
                    else
                        draw(p);
                }

                obj.Handler.Location = point2;
                obj.Handler.parent = par;

                return;
            }

            private void drawText(PointF offset, DeadDog.PDF.TextLine obj)
            {
                if (obj.Text == null || obj.Text.Length == 0)
                    return;
                firstpage = false;
                cb.BeginText();
                cb.SetColorFill(new Color(obj.Color));
                cb.SetFontAndSize(obj.Font.iTextSharpFont.BaseFont, obj.Font.Size);
                cb.ShowTextAligned(textAlignment(obj.Alignment), obj.Text, getP(obj.X), currentsize.HeightPoint - getP(obj.Baseline), 0);
                cb.EndText();
            }
            private void drawBox(PointF offset, DeadDog.PDF.Box obj)
            {
                if (!obj.HasBorder && !obj.HasFill)
                    return;
                firstpage = false;
                float y = currentsize.HeightPoint - getP(obj.Size.Height) - getP(obj.Location.Y);

                if (obj.HasBorder)
                {
                    cb.SetLineWidth(obj.BorderWidth);
                    cb.SetColorStroke(new Color(obj.BorderColor));
                }
                cb.SetColorFill(new Color(obj.FillColor));
                cb.Rectangle(getP(obj.Location.X), y, getP(obj.Size.Width), getP(obj.Size.Height));
                if (obj.HasBorder && obj.HasFill)
                    cb.FillStroke();
                else if (obj.HasBorder)
                    cb.Stroke();
                else if (obj.HasFill)
                    cb.Fill();
            }
            private void drawElipse(PointF offset, DeadDog.PDF.Elipse obj)
            {
                if (!obj.HasBorder && !obj.HasFill)
                    return;
                firstpage = false;
                float y = currentsize.HeightPoint - getP(obj.Size.Height) - getP(obj.Location.Y);

                if (obj.HasBorder)
                {
                    cb.SetLineWidth(obj.BorderWidth);
                    cb.SetColorStroke(new Color(obj.BorderColor));
                }
                cb.SetColorFill(new Color(obj.FillColor));
                float x = getP(obj.Location.X);
                float x2 = getP(obj.Size.Width) + x;
                float y2 = getP(obj.Size.Height) + y;
                cb.Ellipse(x, y, x2, y2);
                //cb.Rectangle(getP(obj.Location.X), y, getP(obj.Size.Width), getP(obj.Size.Height));
                if (obj.HasBorder && obj.HasFill)
                    cb.FillStroke();
                else if (obj.HasBorder)
                    cb.Stroke();
                else if (obj.HasFill)
                    cb.Fill();
            }
            private void drawLine(PointF offset, DeadDog.PDF.Line obj)
            {
                firstpage = false;
                cb.SetColorStroke(new Color(obj.Color));
                cb.SetLineWidth(obj.LineWidth);
                cb.MoveTo(getP(obj.X), writer.PageSize.Height - getP(obj.Y));
                cb.LineTo(getP(obj.X + obj.Width), writer.PageSize.Height - getP(obj.Y + obj.Height));
                cb.Stroke();
            }
            private void drawImage(PointF offset, DeadDog.PDF.ImageObject obj)
            {
                firstpage = false;
                iTextSharp.text.Image img;
                if (imagePaths.Contains(obj.Filepath))
                    img = images[imagePaths.IndexOf(obj.Filepath)];
                else
                {
                    img = iTextSharp.text.Image.GetInstance(obj.Filepath);
                    imagePaths.Add(obj.Filepath);
                    images.Add(img);
                }
                img.ScaleAbsolute(getP(obj.Size.Width), getP(obj.Size.Height));

                float x = getP(obj.Location.X);
                float y = currentsize.HeightPoint - getP(obj.Location.Y) - img.ScaledHeight;
                img.SetAbsolutePosition(x, y);
                cb.AddImage(img);
                //document.Add(img);
            }

            private List<iTextSharp.text.Image> images = new List<iTextSharp.text.Image>();
            private List<string> imagePaths = new List<string>();
        }
    }
}
