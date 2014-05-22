using System;
using System.Collections.Generic;
using System.Text;

namespace DeadDog.PDF
{
    public class Page : IPDFPage
    {
        private PageSize pagesize;
        private List<IPDFObject> objects;

        public Page(PageSize pagesize)
        {
            this.pagesize = pagesize;
            this.objects = new List<IPDFObject>();
        }

        public List<IPDFObject> Objects
        {
            get { return objects; }
        }

        #region IPDFPage Members

        public PageSize PageSize
        {
            get { return pagesize; }
        }

        public void Collect(ObjectCollector collector)
        {
            collector.Add(objects);
        }

        #endregion
    }
}
