using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArffSharp
{
    /// <summary>
    /// A single ARFF attribute with specified nominal values that attribute may take.
    /// </summary>
    class ArffAttributeNominal : ArffAttribute
    {
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ArffAttributeNominal"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="name">The name.</param>
        /// <param name="nominalValues">The nominal values.</param>
        public ArffAttributeNominal(int index, string name, IList<string> nominalValues) : base(index, name)
        {
            this.NominalValues = new ReadOnlyCollection<string>(nominalValues);
            this.TypeKeyWord = "";
        }


        /// <summary>
        /// Gets the nominal values.
        /// </summary>
        public ReadOnlyCollection<string> NominalValues { get; private set; }
    }
}
