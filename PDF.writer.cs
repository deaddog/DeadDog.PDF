using System;
using System.Collections.Generic;
using System.Text;

using Color = iTextSharp.text.Color;
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

            public Writer(Document document, string filename, Page[] pages)
            {
                this.document = document;
                try
                {
                    writer = PdfWriter.GetInstance(document, new System.IO.FileStream(filename, System.IO.FileMode.Create));
                    document.Open();
                    cb = writer.DirectContent;

                    foreach (Page page in pages)
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

            private void draw(Page page)
            {
                currentsize = page.PageSize;
                if (!firstpage)
                    document.NewPage();
                firstpage = false;

                PointF offset = PointF.Empty;
                foreach (var obj in page.Objects)
                    draw(offset, (dynamic)obj);
            }

            private void draw<T>(PointF offset, PDFGroup<T> group) where T : PDFObject
            {
                offset = new PointF(offset.X + group.OffsetX, offset.Y + group.OffsetY);
                foreach (var obj in group.GetPDFObjects())
                {
                    var gOff = group.GetGroupingOffset(obj);
                    draw(new PointF(
                        offset.X + gOff.X,
                        offset.Y + gOff.Y),
                        ((dynamic)obj));
                }
            }

            private void draw(PointF offset, DeadDog.PDF.TextLine obj)
            {
                if (obj.Text == null || obj.Text.Length == 0)
                    return;
                firstpage = false;
                cb.BeginText();
                cb.SetColorFill(new Color(obj.Color));
                cb.SetFontAndSize(obj.Font.iTextSharpFont.BaseFont, obj.Font.Size);
                cb.ShowTextAligned(textAlignment(obj.Alignment), obj.Text, (obj.OffsetX + offset.X).ToPoints(), currentsize.HeightPoint - (obj.Baseline + offset.Y).ToPoints(), 0);
                cb.EndText();
            }
            private void draw(PointF offset, DeadDog.PDF.Box obj)
            {
                if (!obj.HasBorder && !obj.HasFill)
                    return;
                firstpage = false;
                float y = currentsize.HeightPoint - (obj.Size.Height + offset.Y + obj.OffsetY).ToPoints();

                if (obj.HasBorder)
                {
                    cb.SetLineWidth(obj.BorderWidth);
                    cb.SetColorStroke(new Color(obj.BorderColor));
                }
                cb.SetColorFill(new Color(obj.FillColor));
                cb.Rectangle((offset.X + obj.OffsetX).ToPoints(), y, obj.Size.Width.ToPoints(), obj.Size.Height.ToPoints());
                if (obj.HasBorder && obj.HasFill)
                    cb.FillStroke();
                else if (obj.HasBorder)
                    cb.Stroke();
                else if (obj.HasFill)
                    cb.Fill();
            }
            private void draw(PointF offset, DeadDog.PDF.Elipse obj)
            {
                if (!obj.HasBorder && !obj.HasFill)
                    return;
                firstpage = false;
                float y = currentsize.HeightPoint - obj.Size.Height.ToPoints() - (obj.OffsetY + offset.Y).ToPoints();

                if (obj.HasBorder)
                {
                    cb.SetLineWidth(obj.BorderWidth);
                    cb.SetColorStroke(new Color(obj.BorderColor));
                }
                cb.SetColorFill(new Color(obj.FillColor));
                float x = (obj.OffsetX + offset.X).ToPoints();
                float x2 = obj.Size.Width.ToPoints() + x;
                float y2 = obj.Size.Height.ToPoints() + y;
                cb.Ellipse(x, y, x2, y2);

                if (obj.HasBorder && obj.HasFill)
                    cb.FillStroke();
                else if (obj.HasBorder)
                    cb.Stroke();
                else if (obj.HasFill)
                    cb.Fill();
            }
            private void draw(PointF offset, DeadDog.PDF.Line obj)
            {
                float x = (offset.X + obj.OffsetX).ToPoints();
                float y = (offset.Y + obj.OffsetY).ToPoints();

                firstpage = false;
                cb.SetColorStroke(new Color(obj.Color));
                cb.SetLineWidth(obj.LineWidth);
                cb.MoveTo(x, writer.PageSize.Height - y);
                cb.LineTo(x + obj.Width.ToPoints(), writer.PageSize.Height - y - obj.Height.ToPoints());
                cb.Stroke();
            }
            private void draw(PointF offset, DeadDog.PDF.ImageObject obj)
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
                img.ScaleAbsolute(obj.Size.Width.ToPoints(), obj.Size.Height.ToPoints());

                float x = (obj.OffsetX + offset.X).ToPoints();
                float y = currentsize.HeightPoint - (obj.OffsetY + offset.Y).ToPoints() - img.ScaledHeight;
                img.SetAbsolutePosition(x, y);
                cb.AddImage(img);
            }

            private List<iTextSharp.text.Image> images = new List<iTextSharp.text.Image>();
            private List<string> imagePaths = new List<string>();
        }
    }
}
