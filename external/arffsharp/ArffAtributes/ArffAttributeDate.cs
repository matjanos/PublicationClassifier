using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArffSharp
{
    class ArffAttributeDate : ArffAttribute
    {
        

        /// <summary>
        /// Initializes a new instance of the <see cref="ArffAttributeDate"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="name">The name.</param>
        public ArffAttributeDate(int index, string name) : base(index, name)
        {
            this.TypeKeyWord = "date";
        }
    }
}
