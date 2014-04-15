namespace ArffSharp
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// A single ARFF attribute.
    /// </summary>
    public class ArffAttribute
    {
        /// <summary>
        /// Gets the index.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }


        /// <summary>
        /// Gets the nominal values.
        /// </summary>
        public ReadOnlyCollection<string> NominalValues { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArffAttribute"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="name">The name.</param>
        /// <param name="nominalValues">The nominal values.</param>
        public ArffAttribute(int index, string name, IList<string> nominalValues)
        {
            this.Index = index;
            this.Name = name;
            this.NominalValues = new ReadOnlyCollection<string>(nominalValues);
        }

        public override string ToString()
        {
            return this.Index + ". " + this.Name;
        }
    }
}
