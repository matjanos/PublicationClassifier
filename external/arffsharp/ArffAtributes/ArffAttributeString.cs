using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArffSharp
{
    class ArffAttributeString : ArffAttribute
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="ArffAttributeString"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="name">The name.</param>
        public ArffAttributeString(int index, string name)
            : base(index, name)
        {
            TypeKeyWord = "string";
        }
    }
}
