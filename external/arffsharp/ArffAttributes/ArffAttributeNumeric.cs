namespace ArffSharp.ArffAttributes
{
    class ArffAttributeNumeric : ArffAttribute
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ArffAttributeNumeric"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="name">The name.</param>
        public ArffAttributeNumeric(int index, string name)
            : base(index, name)
        {
            TypeKeyWord = "numeric";
        }
    }
}
