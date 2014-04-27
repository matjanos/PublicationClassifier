using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArffSharp
{
    public class ArffValueDate : IArffValue
    {
        public int AttributeNo
        {
            get;
            private set;
        }
        public DateTime Value
        {
            get;
            private set;
        }


        public ArffValueDate(string sparseValue)
        {
            var attributeWithNo = sparseValue.Split(' ');
            if (attributeWithNo.Length != 2) throw new ArgumentException("This string argument does not represent Sparse attribute value");
            AttributeNo = Convert.ToInt32(attributeWithNo[0]);
           // this.Value = DateTime.Parse(attributeWithNo[1], CultureInfo.InvariantCulture);//TODO: parse date
        }

        public ArffValueDate(DateTime value, int attributeNo)
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
            throw new ImproperValueTypeException("This value is of another type: "+Value.GetType().ToString());
        }

        public DateTime GetDateValue()
        {
            return Value;
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
