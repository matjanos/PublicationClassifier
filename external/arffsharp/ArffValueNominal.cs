using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArffSharp
{
    public class ArffValueNominal : IArffValue
    {
        public int Value
        {
            get;
            private set;
        }
        public ArffValueNominal(int value, int attributeNo)
        {
            this.Value = value;
            this.AttributeNo = attributeNo;
        }

        public int AttributeNo
        {
            get;
            private set;
        }


        public object ValueObj
        {
            get { return Value; }
        }
    }
}
