﻿namespace ArffSharp
{
    /// <summary>
    /// Represents a single data row in an ARFF file.
    /// </summary>
    public class ArffRecord : IArffRecord
    {
        public ArffRecord(int valuesCount)
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


        IArffValue[] IArffRecord.getValues()
        {
            return this.Values;
        }
    }
}
