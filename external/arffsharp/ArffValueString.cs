using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArffSharp
{
    public class ArffValueString : IArffValue
    {
         public int AttributeNo
        {
            get;
            private set;
        }
        public string Value
        {
            get;
            private set;
        }


        public ArffValueString(string sparseValue)
        {
            var attributeWithNo = sparseValue.Split(' ');
            if (attributeWithNo.Length != 2) throw new ArgumentException("This string argument does not represent Sparse attribute value");
            AttributeNo = Convert.ToInt32(attributeWithNo[0]);
            this.Value = attributeWithNo[1];
        }

        public ArffValueString(string value, int attributeNo)
        {
            Value = value;
            AttributeNo = attributeNo;
        }


        public object ValueObj
        {
            get { return Value; }
        }
    }
}
