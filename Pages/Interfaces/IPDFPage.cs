using System;
using System.Collections.Generic;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Defines methods and properties required by all pdf pages.
    /// </summary>
    public interface IPDFPage
    {
        /// <summary>
        /// Gets the <see cref="PageSize"/> of the pdf page.
        /// </summary>
        PageSize PageSize
        {
            get;
        }

        /// <summary>
        /// Collects all pdf objects that should be displayed on the page.
        /// </summary>
        /// <param name="collector">An <see cref="ObjectCollector"/> to which all pdf objects are added.</param>
        void Collect(ObjectCollector collector);
    }
}
