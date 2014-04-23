using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWM.PubClassifier.DataAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            ArffConnection ac = new ArffConnection("D:/a.arff");
            ac.getDataTable();
        }
    }
}
