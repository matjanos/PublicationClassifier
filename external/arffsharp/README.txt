ArffSharp - A .NET reader for the Attribute Relationship File Format (ARFF) used by Weka.

Developed mainly by	Ian Obermiller. Thanks to him!

Improved by Kuba Matjanowski:
	-Non-nominal attribute values 
	-support for Sparse ARFF files
All changes according to Arff file specification: http://www.cs.waikato.ac.nz/ml/weka/arff.html

Expected development:
	-performance improvement
	-instances weights
	-relational attributes
	-other numeric attributes (real, integer) support

Many thanks to Sebastien Lorion's fantastic CsvReader: http://www.codeproject.com/Articles/9258/A-Fast-CSV-Reader

Sample:

    using System;
    using ArffSharp;

    public class Program
    {
        public static void Main(string[] args)
        {
            using (ArffReader reader = new ArffReader(@"data\training.arff"))
            {
                ArffRecord record;
                while ((record = reader.ReadNextRecord()) != null)
                {
                    foreach (var arffValue in values)
					{
						if (arffValue == null) 
							continue;
						var attribute = reader.Attributes[arffValue.AttributeNo];
						Console.WriteLine("Attribute no.{0}[{1}]: {2}",arffValue.AttributeNo, attribute.Name, arffValue.ValueObj);
					}
                    Console.WriteLine();
                }
            }
        }
    }