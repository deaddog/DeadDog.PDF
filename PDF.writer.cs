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

            private Vector2D currentsize;

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
                if (page.PageSize != currentsize)
                {
                    currentsize = page.PageSize;

                    var rectangle = new iTextSharp.text.Rectangle(
                        (float)currentsize.X.Value(UnitsOfMeasure.Points),
                        (float)currentsize.Y.Value(UnitsOfMeasure.Points));

                    document.SetPageSize(rectangle);
                }

                document.NewPage();

                Vector2D offset = Vector2D.Zero;
                foreach (var obj in page.Objects)
                    draw(offset, (dynamic)obj);
            }

            private void draw<T>(Vector2D offset, PDFGroup<T> group) where T : PDFObject
            {
                offset += group.Offset;
                foreach (var obj in group.GetPDFObjects())
                {
                    var gOff = group.GetGroupingOffset(obj);
                    draw(offset + gOff, (dynamic)obj);
                }
            }

            private void draw(Vector2D offset, LeafObject obj)
            {
                var stroke = obj as StrokeObject;
                var fill = obj as FillObject;

                bool hasstroke = stroke?.HasBorder ?? false;
                bool hasfill = fill?.HasFill ?? false;

                if (!hasstroke && !hasfill)
                    return;

                if (hasstroke)
                {
                    cb.SetLineWidth(stroke.BorderWidth);
                    cb.SetColorStroke(new Color(stroke.BorderColor));
                }
                if (hasfill)
                    cb.SetColorFill(new Color(fill.FillColor));

                offset += obj.Offset;
                offset.Y = currentsize.Y - obj.Size.Y - offset.Y;

                obj.Render(cb, offset);

                if (hasstroke && hasfill)
                    cb.FillStroke();
                else if (hasstroke)
                    cb.Stroke();
                else if (hasfill)
                    cb.Fill();
            }

            private void draw(PointF offset, DeadDog.PDF.TextLine obj)
            {
                if (obj.Text == null || obj.Text.Length == 0)
                    return;
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

                cb.SetColorStroke(new Color(obj.BorderColor));
                cb.SetLineWidth(obj.BorderWidth);
                cb.MoveTo(x, writer.PageSize.Height - y);
                cb.LineTo(x + obj.Width.ToPoints(), writer.PageSize.Height - y - obj.Height.ToPoints());
                cb.Stroke();
            }
            private void draw(PointF offset, DeadDog.PDF.ImageObject obj)
            {
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
