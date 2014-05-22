using System;
using System.Collections.Generic;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Provides methods to collect a range of <see cref="IPDFObject"/> elements.
    /// </summary>
    public class ObjectCollector
    {
        private List<IPDFObject> objects;
        internal IPDFObject[] GetObjects
        {
            get { return objects.ToArray(); }
        }

        /// <summary>
        /// Initializes a new instance of the DeadDog.PDF.ObjectCollector class.
        /// </summary>
        public ObjectCollector()
        {
            this.objects = new List<IPDFObject>();
        }

        /// <summary>
        /// Adds an object to the the DeadDog.PDF.ObjectCollector.
        /// </summary>
        /// <param name="item">The object to be added to the DeadDog.PDF.ObjectCollector.
        /// The value can be null.</param>
        public void Add(IPDFObject item)
        {
            objects.Add(item);
        }
        /// <summary>
        /// Adds the elements of the specified collection to the DeadDog.PDF.ObjectCollector.
        /// </summary>
        /// <param name="collection">The collection whose elements should be added to the DeadDog.PDF.ObjectCollector.
        /// The collection itself cannot be null, but it can contain elements that are null.</param>
        public void Add(params IPDFObject[] collection)
        {
            objects.AddRange(collection);
        }
        /// <summary>
        /// Adds the elements of the specified collection to the DeadDog.PDF.ObjectCollector.
        /// </summary>
        /// <param name="collection">The collection whose elements should be added to the DeadDog.PDF.ObjectCollector.
        /// The collection itself cannot be null, but it can contain elements that are null.</param>
        public void Add(IEnumerable<IPDFObject> collection)
        {
            objects.AddRange(collection);
        }
    }
}
