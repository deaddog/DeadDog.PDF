using System.Collections.Generic;

namespace DeadDog.PDF
{
    public class Page
    {
        private Vector2D pagesize;
        private List<PDFObject> objects;

        public Page(Vector2D pagesize)
        {
            this.pagesize = pagesize;
            this.objects = new List<PDFObject>();
        }

        public List<PDFObject> Objects
        {
            get { return objects; }
        }

        public Vector2D PageSize
        {
            get { return pagesize; }
        }
    }
}
