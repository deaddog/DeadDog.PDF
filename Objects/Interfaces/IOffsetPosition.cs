using System;
using System.Collections.Generic;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Provides methods allowing an objects X and Y properties to be offset by a value when using the <see cref="LocationHandler"/>.
    /// </summary>
    public interface IOffsetPosition
    {
        /// <summary>
        /// Gets the distance the objects X property will be offset.
        /// </summary>
        float OffsetX { get; }
        /// <summary>
        /// Gets the distance the objects Y property will be offset.
        /// </summary>
        float OffsetY { get;}
    }
}
