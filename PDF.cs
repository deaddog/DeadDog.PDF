using System;
using System.Collections.Generic;
using System.Text;

namespace DeadDog.PDF
{
    public partial class PDFDocument
    {
        public class PageCollection : IEnumerable<IPDFPage>
        {
            private List<IPDFPage> objects = new List<IPDFPage>();

            public T Add<T>(T value) where T : IPDFPage
            {
                objects.Add(value);
                return value;
            }
            public void Add(params IPDFPage[] values)
            {
                foreach (IPDFPage value in values)
                    Add(value);
            }

            public bool Remove(IPDFPage value)
            {
                return objects.Remove(value);
            }
            public void Remove(int index)
            {
                objects.RemoveAt(index);
            }

            public bool Contains(IPDFPage value)
            {
                return objects.Contains(value);
            }
            public IPDFPage this[int index]
            {
                get { return objects[index]; }
            }
            public int Count
            {
                get { return objects.Count; }
            }

            public IPDFPage[] ToArray()
            {
                return objects.ToArray();
            }
            
            public IEnumerator<IPDFPage> GetEnumerator()
            {
                return objects.GetEnumerator();
            }
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return objects.GetEnumerator();
            }
        }

        public PageCollection Pages = new PageCollection();
	
        public PDFDocument()
        { }

        public void Create(string filename)
        {
            if (Pages.Count == 0)
                throw new Exception("No pages added.");

            Writer w = new Writer(new iTextSharp.text.Document(Pages[0].PageSize.SizePoint), filename, Pages.ToArray());
        }
    }
}
