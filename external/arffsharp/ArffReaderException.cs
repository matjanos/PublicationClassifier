namespace ArffSharp
{
    using System;

    /// <summary>
    /// Exception thrown by the ArffReader.
    /// </summary>
    public class ArffReaderException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArffReaderException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ArffReaderException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArffReaderException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ArffReaderException(string message, Exception innerException) : base(message, innerException) { }
    }
}
