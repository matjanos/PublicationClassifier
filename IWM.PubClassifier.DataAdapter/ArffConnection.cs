using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ArffSharp;
using ArffSharp.ArffAttributes;

namespace IWM.PubClassifier.DataAdapter
{
    internal class ArffConnection : IDisposable
    {
        //Rozszerzenie plików Arff
        private const string Extension = ".arff";
        public List<Dictionary<string, float>> ObjectsAttributes;


        //Ścieżka do pliku Arff
        private string _path;
        //czytnik plików Arff
        private ArffReader _reader;

        /// <summary>
        ///     Tworzy obiekt typu ArffConnection, który realizuje dostęp do pliku Arff i pobiera z niego dane
        /// </summary>
        /// <param name="pathString">Ścieżka do pliku .arff</param>
        public ArffConnection(string pathString)
        {
            if (pathString == null) throw new ArgumentNullException();
            string pathTemp = Path.GetFullPath(pathString);
            if (Path.GetExtension(pathTemp) != Extension)
                throw new InvalidDataException("The file has to have " + Extension + " extension");
            if (!File.Exists(pathTemp)) throw new FileNotFoundException(pathTemp + " not found.");

            _path = pathTemp;

            _reader = new ArffReader(_path);
            InsertDataIntoHashTables();
        }

        public void Dispose()
        {
            _path = null;
            _reader.Dispose();
            _reader = null;
        }

        /// <summary>
        ///     Inserts data into Dictionaries from the arff source file.
        /// </summary>
        private void InsertDataIntoHashTables()
        {
            var clock = new Stopwatch();
            clock.Start();
            IArffRecord record;
            ObjectsAttributes = new List<Dictionary<string, float>>();
            while ((record = _reader.ReadNextRecord()) != null)
            {
                var recordDictionary = new Dictionary<string, float>();
                var values = record.GetValues();
                foreach (var arffValue in values)
                {
                    if (arffValue == null) continue;

                    var attr = _reader.Attributes[arffValue.AttributeNo];
                    if (arffValue is ArffValueNumeric)
                    {
                        recordDictionary.Add(attr.Name, (float)arffValue.ValueObj);
                    }
                    else if (arffValue is ArffValueDate)
                    {
                        Console.WriteLine(((ArffValueDate)arffValue).GetDateValue());
                    }
                    else if (arffValue is ArffValueString)
                    {
                        Console.WriteLine(((ArffValueString)arffValue).GetStringValue());

                    }
                    else if (arffValue is ArffValueNominal)
                    {
                        Console.WriteLine(String.Format("{0} : {1}",_reader.Attributes[arffValue.AttributeNo].Name,((ArffAttributeNominal)_reader.Attributes[arffValue.AttributeNo]).NominalValues[((ArffValueNominal)arffValue).GetNominalValue()]));
                    }
                }
                ObjectsAttributes.Add(recordDictionary);
            }
            clock.Stop();
            Console.WriteLine("Czas wczytywania: {0}", clock.Elapsed);
            //Console.ReadKey();
        }
    }
}