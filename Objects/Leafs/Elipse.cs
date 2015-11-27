namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw an elipse in a pdf document.
    /// </summary>
    public class Elipse : FillObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Elipse"/> class.
        /// </summary>
        /// <param name="size">The size of the elipse.</param>
        public Elipse(Vector2D size)
            : base(Vector2D.Zero, size)
        {
        }

        protected override void Render(ContentWriter cw, Vector2D offset)
        {
            cw.Ellipse(offset, Size);
        }
    }
}
