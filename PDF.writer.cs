using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

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
                offset += obj.Offset;
                offset.Y = currentsize.Y - obj.Size.Y - offset.Y;

                obj.Render(cb, offset);
            }
        }
    }
}
