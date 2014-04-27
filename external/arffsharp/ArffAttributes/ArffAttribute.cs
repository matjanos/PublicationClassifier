using System.Collections.ObjectModel;

namespace ArffSharp.ArffAttributes
{
    /// <summary>
    /// A single ARFF attribute.
    /// </summary>
    public abstract class ArffAttribute
    {
        public enum ArffAttributeType
        {
            //type of the attribute
            Date = 1,
            Numeric = 2,
            String = 3
            , Nominal = 4
        }

        /// <summary>
        /// Key word that indicates the specific type
        /// </summary>
        public string TypeKeyWord{get; protected set;}

        /// <summary>
        /// Gets the index.
        /// </summary>
        public int Index { get; protected set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; protected set; }


        public ArffAttribute(int index, string name)
        {
            this.Index = index;
            this.Name = name;
        }

        public override string ToString()
        {
            return this.Index + ". " + this.Name;
        }

    }
}
