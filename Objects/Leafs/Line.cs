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
            : base(Vector2D.Zero, size)
        {
        }

        protected override void Render(ContentWriter cw, Vector2D offset)
        {
            cw.MoveTo(offset.X, offset.Y + Size.Y);
            cw.LineTo(offset.X + Size.X, offset.Y);
        }
    }
}
