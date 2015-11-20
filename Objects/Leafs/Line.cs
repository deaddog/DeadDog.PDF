namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw a line in a pdf document.
    /// </summary>
    public class Line : StrokeObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="size">The size of the line.</param>
        public Line(Vector2D size)
            : base(true, Vector2D.Zero, size)
        {
        }
    }
}
