﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Implements basic functionalities for pdf groups.
    /// Groups are collections of <see cref="IPDFObject"/> objects
    /// In a <see cref="PDFGroup"/> objects are collected in the protected list property.
    /// </summary>
    public abstract class PDFGroup : IPDFGroup
    {
        private PointF offset;
        private PDFList<IPDFObject> privatelist;
        /// <summary>
        /// The <see cref="PDFList{IPDFObject}"/> to which all objects in this group should be added.
        /// </summary>
        protected PDFList<IPDFObject> list
        {
            get { return privatelist; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFGroup"/> class.
        /// </summary>
        /// <param name="offsetX">The x offset for this object.</param>
        /// <param name="offsetY">The y offset for this object.</param>
        public PDFGroup(float offsetX, float offsetY)
            : this(new PointF(offsetX, offsetY))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PDFGroup"/> class.
        /// </summary>
        /// <param name="offset">The offset for this object.</param>
        public PDFGroup(PointF offset)
        {
            this.offset = offset;
            this.privatelist = new PDFList<IPDFObject>(this);
        }

        #region IPDFGroup Members

        /// <summary>
        /// Returns the location of any element within this <see cref="PDFGroup"/>.
        /// </summary>
        /// <param name="obj">The element whichs location is returned.</param>
        /// <returns>The location of obj.</returns>
        public abstract System.Drawing.PointF GetLocation(IPDFObject obj);

        #endregion

        #region IPDFObject Members

        /// <summary>
        /// Gets or sets the offset for this <see cref="PDFGroup"/>.
        /// </summary>
        public PointF Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        /// <summary>
        /// Gets or sets the x offset for this <see cref="PDFGroup"/>.
        /// </summary>
        public float OffsetX
        {
            get { return offset.X; }
            set { offset.X = value; }
        }
        /// <summary>
        /// Gets or sets the y offset for this <see cref="PDFGroup"/>.
        /// </summary>
        public float OffsetY
        {
            get { return offset.Y; }
            set { offset.Y = value; }
        }

        /// <summary>
        /// Gets the width of this <see cref="PDFGroup"/>.
        /// </summary>
        public abstract float Width
        {
            get;
        }
        /// <summary>
        /// Gets the height of this <see cref="PDFGroup"/>.
        /// </summary>
        public abstract float Height
        {
            get;
        }

        /// <summary>
        /// Collects all pdf objects contained within this group.
        /// </summary>
        /// <param name="collector">An <see cref="ObjectCollector"/> to which all pdf objects are added.</param>
        public void Collect(ObjectCollector collector)
        {
            collector.Add(list);
        }

        #endregion
    }

    /// <summary>
    /// Implements basic functionalities for pdf groups using generic types.
    /// Groups are collections of <see cref="IPDFObject"/> objects
    /// In a <see cref="PDFGroup{T}"/> objects are collected in the protected list property.
    /// </summary>
    /// <typeparam name="T">The type of elements in the pdf group</typeparam>
    public abstract class PDFGroup<T> : IPDFGroup<T> where T : IPDFObject
    {
        private PointF offset;
        private PDFList<T> privatelist;
        /// <summary>
        /// The <see cref="PDFList{T}"/> to which all objects in this group should be added.
        /// </summary>
        protected PDFList<T> list
        {
            get { return privatelist; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFGroup{T}"/> class.
        /// </summary>
        /// <param name="offsetX">The x offset for this object.</param>
        /// <param name="offsetY">The y offset for this object.</param>
        public PDFGroup(float offsetX, float offsetY)
            : this(new PointF(offsetX, offsetY))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PDFGroup{T}"/> class.
        /// </summary>
        /// <param name="offset">The offset for this object.</param>
        public PDFGroup(PointF offset)
        {
            this.offset = offset;
            this.privatelist = new PDFList<T>(this);
        }

        #region IPDFGroup Members

        /// <summary>
        /// Returns the location of any element within this <see cref="PDFGroup{T}"/> by checking if it is of type T. If it is GetLocation(T obj) is returned.
        /// </summary>
        /// <param name="obj">The element whichs location is returned.</param>
        /// <returns>The location of obj.</returns>
        public System.Drawing.PointF GetLocation(IPDFObject obj)
        {
            if (obj is T)
                return GetLocation((T)obj);
            else
                throw new ArgumentException("obj is not of type " + typeof(T).ToString() + ".", "obj");
        }

        #endregion

        #region IPDFObject Members

        /// <summary>
        /// Gets or sets the offset for this <see cref="PDFGroup{T}"/>.
        /// </summary>
        public PointF Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        /// <summary>
        /// Gets or sets the x offset for this <see cref="PDFGroup{T}"/>.
        /// </summary>
        public float OffsetX
        {
            get { return offset.X; }
            set { offset.X = value; }
        }
        /// <summary>
        /// Gets or sets the y offset for this <see cref="PDFGroup{T}"/>.
        /// </summary>
        public float OffsetY
        {
            get { return offset.Y; }
            set { offset.Y = value; }
        }

        /// <summary>
        /// Gets the width of this <see cref="PDFGroup{T}"/>.
        /// </summary>
        public abstract float Width
        {
            get;
        }
        /// <summary>
        /// Gets the height of this <see cref="PDFGroup{T}"/>.
        /// </summary>
        public abstract float Height
        {
            get;
        }

        /// <summary>
        /// Collects all pdf objects contained within this group.
        /// </summary>
        /// <param name="collector">An <see cref="ObjectCollector"/> to which all pdf objects are added.</param>
        public void Collect(ObjectCollector collector)
        {
            foreach (T t in privatelist)
            {
                IPDFObject o = (IPDFObject)t;
                collector.Add(o);
            }
        }

        #endregion

        #region IPDFGroup<T> Members

        /// <summary>
        /// Returns the location of any element within this <see cref="PDFGroup{T}"/>.
        /// </summary>
        /// <param name="obj">The element whichs location is returned.</param>
        /// <returns>The location of obj.</returns>
        public abstract System.Drawing.PointF GetLocation(T obj);

        #endregion
    }
}
