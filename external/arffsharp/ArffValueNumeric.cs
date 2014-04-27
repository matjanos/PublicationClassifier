using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ArffSharp
{
    public class ArffValueNumeric : IArffValue
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
        public float GetNumericValue()
        {
            return Value;
        }

        public DateTime GetDateValue()
        {
            throw new ImproperValueTypeException("This value is of another type: " + Value.GetType().ToString());
        }

        public string GetStringValue()
        {
            throw new ImproperValueTypeException("This value is of another type: " + Value.GetType().ToString());
        }

        public int GetNominalValue()
        {
            throw new ImproperValueTypeException("This value is of another type: " + Value.GetType().ToString());
        }
    }
}
