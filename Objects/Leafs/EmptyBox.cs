namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information on the size of a box. The box is not drawn but only used as a space buffer.
    /// </summary>
    public sealed class EmptyBox : PDFObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyBox"/> class.
        /// </summary>
        /// <param name="size">The size of the box.</param>
        public EmptyBox(Vector2D size)
            : base(Vector2D.Zero, size)
        {
        }
    }
}
