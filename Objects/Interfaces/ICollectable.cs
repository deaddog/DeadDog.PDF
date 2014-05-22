using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeadDog.PDF
{
    interface ICollectable
    {
        /// <summary>
        /// Collects all pdf objects contained within this object.
        /// </summary>
        /// <param name="collector">An <see cref="ObjectCollector"/> to which all pdf objects are added.</param>
        void Collect(ObjectCollector collector);
    }
}
