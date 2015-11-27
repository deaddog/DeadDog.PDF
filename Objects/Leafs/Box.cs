namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw a box in a pdf document.
    /// </summary>
    public class Box : FillObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class.
        /// </summary>
        /// <param name="size">The size of the box.</param>
        public Box(Vector2D size)
            : base(Vector2D.Zero, size)
        {
        }

        protected override void Render(ContentWriter cw, Vector2D offset)
        {
            cw.Rectangle(offset, Size);
        }
    }
}
