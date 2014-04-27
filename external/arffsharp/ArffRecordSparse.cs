
using System;
using System.Collections.ObjectModel;
using ArffSharp.ArffAttributes;

namespace ArffSharp
{
    class ArffRecordSparse : ArffRecord
    {
        public ArffRecordSparse(ReadOnlyCollection<ArffAttribute> attributes):base(attributes)
        {
        }
        
        public IArffValue[] GetValues()
        {
            return Values;
        }


        public override void AddValue(string value)
        {
            var pair = value.Split(new []{' '}, 2);
            Iterator=Convert.ToInt32(pair[0]);
            base.AddValue(pair[1]);
        }
    }
}
