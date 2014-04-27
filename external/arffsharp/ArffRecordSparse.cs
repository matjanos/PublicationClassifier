
namespace ArffSharp
{
    class ArffRecordSparse : IArffRecord
    {
        public ArffRecordSparse(int valuesCount)
        {
            this.Values = new IArffValue[valuesCount];
        }
        public IArffValue[] Values { get; private set; }

        //public int Weight { private get; private set; }//TODO: http://weka.wikispaces.com/ARFF+(stable+version)#Instance weights in ARFF files
        
        public void addValue(IArffValue value)
        {
            this.Values[value.AttributeNo] = value;
        }

        public IArffValue[] getValues()
        {
            return this.Values;
        }
    }
}
