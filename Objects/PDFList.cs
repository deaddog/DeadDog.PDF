using System;
using System.Collections.Generic;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Inherits <see cref="List{T}" /> and performs internal communication with <see cref="LocationHandler" />.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class PDFList<T> : List<T>,IEnumerable<T> where T : IPDFObject
    {
        private IPDFGroup parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFList{T}" /> class that is empty and has the default initial capacity.
        /// </summary>
        /// <param name="parent">The group that is the owner of this list.</param>
        public PDFList(IPDFGroup parent)
            : base()
        {
            lists.Add(this);
            initialize(parent);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PDFList{T}" /> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="parent">The group that is the owner of this list.</param>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        public PDFList(IPDFGroup parent, IEnumerable<T> collection)
            : base()
        {
            lists.Add(this);
            initialize(parent);
            AddRange(collection);
        }
        //public PDFList(IPDFGroup<T> parent, int capacity) : base(capacity) { initialize(parent); }
        
        private void initialize(IPDFGroup parent)
        {
            if (parent == null)
                throw new Exception("Parameter parent can not be null");
            this.parent = parent;
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        new public T this[int index]
        {
            get { return base[index]; }
            set
            {
                IPDFObject remove = base[index];
                IPDFObject insert = value;
                base[index] = (T)insert;
                remove.Handler.parent = null;
                insert.Handler.parent = (IPDFGroup<IPDFObject>)parent;
            }
        }

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="PDFList{T}" />.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="PDFList{T}" />. The value can be null for reference types.</param>
        /// <returns>The zero-based index of the first occurrence of item within the entire <see cref="PDFList{T}" />, if found; otherwise, –1.</returns>
        public int IndexOf(IPDFObject item)
        {
            if (item is T)
                return base.IndexOf((T)item);
            else
                return -1;
        }

        /// <summary>
        /// Adds an object to the end of the <see cref="PDFList{T}" />.
        /// </summary>
        /// <param name="item">The object to be added to the end of the <see cref="PDFList{T}" />.
        /// The value can be null for reference types.</param>
        new public void Add(T item)
        {
            if (item.Handler.parent != null)
                SRemove(item);

            base.Add(item);
            item.Handler.parent = parent;
        }
        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="PDFList{T}" />.
        /// </summary>
        /// <param name="collection">The collection whose elements should be added to the end of the <see cref="PDFList{T}" />.
        /// The collection itself cannot be null, but it can contain elements that are null, if type T is a reference type.</param>
        new public void AddRange(IEnumerable<T> collection)
        {
            foreach (T item in collection)
                if (item.Handler.parent != null)
                    SRemove(item);

            base.AddRange(collection);
            foreach (T item in collection)
                item.Handler.parent = parent;
        }
        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="PDFList{T}" />.
        /// </summary>
        /// <param name="collection">The collection whose elements should be added to the end of the <see cref="PDFList{T}" />.
        /// The collection itself cannot be null, but it can contain elements that are null, if type T is a reference type.</param>
        public void AddRange(params T[] collection)
        {
            foreach (T item in collection)
                if (item.Handler.parent != null)
                    SRemove(item);

            base.AddRange(collection);
            foreach (T item in collection)
                item.Handler.parent = parent;
        }

        /// <summary>
        /// Removes all elements from the <see cref="PDFList{T}" />.
        /// </summary>
        new public void Clear()
        {
            foreach (T item in this)
                item.Handler.parent = null;
            base.Clear();
        }

        /// <summary>
        /// Inserts an element into the <see cref="PDFList{T}" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        new public void Insert(int index, T item)
        {
            if (item.Handler.parent != null)
                SRemove(item);

            base.Insert(index, item);
            item.Handler.parent = parent;
        }
        /// <summary>
        /// Inserts the elements of a collection into the <see cref="PDFList{T}" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the new elements should be inserted.</param>
        /// <param name="collection">The collection whose elements should be inserted into the <see cref="PDFList{T}" />.
        /// The collection itself cannot be null, but it can contain elements that are null, if type T is a reference type.</param>
        new public void InsertRange(int index, IEnumerable<T> collection)
        {
            foreach (T item in collection)
                if (item.Handler.parent != null)
                    SRemove(item);

            base.InsertRange(index, collection);
            foreach (T item in collection)
                item.Handler.parent = parent;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="PDFList{T}" />.
        /// </summary>
        /// <param name="item">The object to remove from the System.<see cref="PDFList{T}" />. The value can be null for reference types.</param>
        /// <returns>true if item is successfully removed; otherwise, false. This method also returns false if item was not found in the <see cref="PDFList{T}" />.</returns>
        new public bool Remove(T item)
        {
            item.Handler.parent = null;
            bool b = base.Remove(item);
            return b;
        }
        /// <summary>
        /// Removes the all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="match">The <see cref="System.Predicate{T}"/> delegate that defines the conditions of the elements to remove.</param>
        /// <returns>The number of elements removed from the <see cref="PDFList{T}" />.</returns>
        new public int RemoveAll(Predicate<T> match)
        {
            List<T> list = base.FindAll(match);
            foreach (T item in list)
                Remove(item);
            return list.Count;
        }
        /// <summary>
        /// Removes the element at the specified index of the <see cref="PDFList{T}" />.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        new public void RemoveAt(int index)
        {
            this[index].Handler.parent = null;
            base.RemoveAt(index);
        }
        /// <summary>
        /// Removes a range of elements from the <see cref="PDFList{T}" />.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
        /// <param name="count">The number of elements to remove.</param>
        new public void RemoveRange(int index, int count)
        {
            for (int i = index; i < index + count; i++)
                this[i].Handler.parent = null;
            base.RemoveRange(index, count);
        }

        private static List<PDFList<T>> lists = new List<PDFList<T>>();
        private static bool SRemove(T item)
        {
            foreach (List<T> l in lists)
                if (l.Contains(item))
                    return l.Remove(item);
            return false;
        }
    }
}
