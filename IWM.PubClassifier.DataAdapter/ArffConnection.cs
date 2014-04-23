using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using ArffSharp;

namespace IWM.PubClassifier.DataAdapter
{
    class ArffConnection : IDisposable
    {
        //Rozszerzenie plików Arff
        private const string extension = ".arff";

        //Ścieżka do pliku Arff
        private string path = null;
        //czytnik plików Arff
        private ArffReader reader = null;

        /// <summary>
        /// Tworzy obiekt typu ArffConnection, który realizuje dostęp do pliku Arff i pobiera z niego dane
        /// </summary>
        /// <param name="path">Ścieżka do pliku .arff</param>
        public ArffConnection(string pathString)
        {
            if (pathString == null) throw new ArgumentNullException();
            string pathTemp = Path.GetFullPath(pathString);
            if (Path.GetExtension(pathTemp) != extension) throw new InvalidDataException("The file has to have " + extension + " extension");
            if (!File.Exists(pathTemp)) throw new FileNotFoundException(pathTemp + " not found.");

            this.path = pathTemp;

            reader = new ArffReader(@path);
        }

        /// <summary>
        /// Gets data table values from the arff source file.
        /// </summary>
        //TODO: replace void into DataTable
        public /*DataTable*/ void getDataTable()
        {
            IArffRecord record;
            while ((record = reader.ReadNextRecord()) != null)
            {
                IArffValue[] values = (IArffValue[])record.getValues();
                for (int valueIndex = 0; valueIndex < values.Length; valueIndex++)
                {
                    IArffValue val = values[valueIndex];
                    var attr = reader.Attributes[valueIndex];
                    if (val is ArffSharp.ArffValueNominal) ;
                    // Console.WriteLine("{0}: {1}", attr.Name, val.AttributeNo >= 0 ? attr.NominalValues[val.AttributeNo] : "?");
                    else
                    {
                        if (val != null)
                            Console.WriteLine("{0}: {1}", attr.Name, val == null ? "?" : val.ValueObj);
                    }
                }
                Console.WriteLine();
            }
        }


        public void Dispose()
        {
            this.path = null;
            this.reader.Dispose();
            this.reader = null;
        }
    }
}
