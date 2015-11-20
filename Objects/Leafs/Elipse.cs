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
            : base(true, Vector2D.Zero, size)
        {
        }
    }
}
