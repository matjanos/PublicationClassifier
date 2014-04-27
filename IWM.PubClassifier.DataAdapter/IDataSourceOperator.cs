using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWM.PubClassifier.DataAdapter
{
    interface IDataSourceOperator
    {
        int ExecuteSqlStatement(string sqlStatement);
    }
}
