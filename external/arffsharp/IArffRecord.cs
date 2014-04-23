using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArffSharp
{
    public interface IArffRecord
    {
        IArffValue[] getValues();
        void addValue(IArffValue value);
    }
}
