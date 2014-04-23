using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ArffSharp
{
    class ArffValueNumeric : IArffValue
    {
        public int AttributeNo
        {
            get;
            private set;
        }
        public float Value
        {
            get;
            private set;
        }

        private IFormatProvider formatProvider = CultureInfo.InvariantCulture.NumberFormat;

        public ArffValueNumeric(string sparseValue)
        {
            var attributeWithNo = sparseValue.Split(' ');
            if (attributeWithNo.Length != 2) throw new ArgumentException("This string argument does not represent Sparse attribute value");
            this.AttributeNo = Convert.ToInt32(attributeWithNo[0]);
            this.Value = float.Parse(attributeWithNo[1], CultureInfo.InvariantCulture);
        }

        public ArffValueNumeric(float value, int attributeNo)
        {
            this.Value = value;
            this.AttributeNo = attributeNo;
        }


        public object ValueObj
        {
            get { return Value; }
        }
    }
}
