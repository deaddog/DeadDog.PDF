using System;
using System.Collections.Generic;
using System.Text;

namespace DeadDog.PDF
{
    public partial class PDFDocument
    {
        public class PageCollection : IEnumerable<Page>
        {
            private List<Page> objects = new List<Page>();

            public T Add<T>(T value) where T : Page
            {
                objects.Add(value);
                return value;
            }
            public void Add(params Page[] values)
            {
                foreach (Page value in values)
                    Add(value);
            }

            public bool Remove(Page value)
            {
                return objects.Remove(value);
            }
            public void Remove(int index)
            {
                objects.RemoveAt(index);
            }

            public bool Contains(Page value)
            {
                return objects.Contains(value);
            }
            public Page this[int index]
            {
                get { return objects[index]; }
            }
            public int Count
            {
                get { return objects.Count; }
            }

            public Page[] ToArray()
            {
                return objects.ToArray();
            }

            public IEnumerator<Page> GetEnumerator()
            {
                return objects.GetEnumerator();
            }
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return objects.GetEnumerator();
            }
        }

        private PageCollection pages;
        public PageCollection Pages
        {
            get { return pages; }
        }
	
        public PDFDocument()
        {
            this.pages = new PageCollection();
        }

        public void Create(string filename)
        {
            if (Pages.Count == 0)
                throw new Exception("No pages added.");

            Writer w = new Writer(new iTextSharp.text.Document(Pages[0].PageSize.SizePoint), filename, Pages.ToArray());
        }
    }
}
