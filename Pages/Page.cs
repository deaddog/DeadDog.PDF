using System;
using System.Collections.Generic;
using System.Text;

namespace DeadDog.PDF
{
    public class Page
    {
        private PageSize pagesize;
        private List<PDFObject> objects;

        public Page(PageSize pagesize)
        {
            this.pagesize = pagesize;
            this.objects = new List<PDFObject>();
        }

        public List<PDFObject> Objects
        {
            get { return objects; }
        }

        public PageSize PageSize
        {
            get { return pagesize; }
        }
    }
}
