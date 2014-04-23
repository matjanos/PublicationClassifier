using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArffSharp
{
    class ArffRecordSparse : IArffRecord
    {
        public ArffRecordSparse(int valuesCount)
        {
            this.Values = new IArffValue[valuesCount];
        }
        public IArffValue[] Values { get; private set; }

        private int iterator = 0;

        //public int Weight { private get; private set; }//TODO: http://weka.wikispaces.com/ARFF+(stable+version)#Instance weights in ARFF files
        
        public void addValue(IArffValue value)
        {
            this.Values[iterator] = value;
            iterator++;
        }

        public IArffValue[] getValues()
        {
            return this.Values;
        }
    }
}
