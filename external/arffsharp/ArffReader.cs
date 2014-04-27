namespace ArffSharp
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using Extensions;
    using LumenWorks.Framework.IO.Csv;

    /// <summary>
    /// A reader for the Attribute Relationship File Format (ARFF) used by Weka.
    /// </summary>
    public class ArffReader : IDisposable
    {
        /// <summary>
        /// Lines starting with this are treated as attributes.
        /// </summary>
        public const string AttributeDeclaration = "@attribute";

        /// <summary>
        /// Any lines after this are treated as data.
        /// </summary>
        public const string DataDeclaration = "@data";

        private Stream stream;
        private StreamReader reader;
        private CsvReader csvReader;
        private int attributeCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArffReader"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public ArffReader(string fileName)
            : this(File.OpenRead(fileName))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArffReader"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public ArffReader(Stream stream)
        {
            this.stream = stream;
            this.reader = new StreamReader(stream);
            this.ReadAttributes();
            this.csvReader = new CsvReader(reader, false, trimmingOptions: ValueTrimmingOptions.All);
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        public ReadOnlyCollection<ArffAttribute> Attributes { get; private set; }

        /// <summary>
        /// Reads the next record.
        /// </summary>
        /// <returns>The record or null if there are no more records.</returns>
        public IArffRecord ReadNextRecord()
        {
            if (!csvReader.ReadNextRecord()) return null;
            IArffRecord record;
            IArffValue arffVal;
            if (csvReader[0].StartsWith("{"))
            {
                record = new ArffRecordSparse(this.attributeCount);
            }
            else
            {
                record = new ArffRecord(this.attributeCount);
            }
            for (int i = 0; i < csvReader.FieldCount; i++)
            {
                string attrValueStr = csvReader[i];
                if (record is ArffRecordSparse)
                {
                    attrValueStr = attrValueStr.TrimStart(new char[] { '{' }).TrimEnd(new char[] { '}' });
                }
                switch (Attributes[i].TypeKeyWord)
                {
                    case "numeric":
                        arffVal = new ArffValueNumeric(attrValueStr);
                        break;
                    case "date":
                        arffVal = new ArffValueDate(attrValueStr);
                        break;
                    case "string":
                        arffVal = new ArffValueString(attrValueStr);
                        break;
                    case "":
                        var val = attrValueStr.Unescape();
                        if (val.Equals("?"))
                        {
                            arffVal = new ArffValueNominal(-1,i);
                        }
                        else
                        {
                            arffVal = new ArffValueNominal(this.Attributes[i].NominalValues.IndexOf(val),i);

                            if (((ArffValueNominal)arffVal).Value == -1)
                            {
                                throw new ArffReaderException("Unknown nominal value \"" + val + "\" for attribute \"" + this.Attributes[i].Name + "\".");
                            }
                        }
                        break;
                    default:
                        throw new Exception("Unknown ARFF attribute type");
                }
                record.addValue(arffVal);
                if (record is ArffRecordSparse && csvReader[i][csvReader[i].Length-1]=='}')
                {//koniec atrybutów wariantu
                    //TODO: czy jest waga
                    break;
                }
            }

            return record;
        }

        /// <summary>
        /// Reads the attributes.
        /// </summary>
        private void ReadAttributes()
        {
            int attributeIndex = 0;
            var attributes = new List<ArffAttribute>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWithI(DataDeclaration))
                {
                    break;
                }

                if (!line.StartsWithI(AttributeDeclaration))
                {
                    continue;
                }

                line = line.Substring(AttributeDeclaration.Length + 1);
                var split = line.Split('{', '}');
                if (split.Length == 1)
                {
                    split[0] = System.Text.RegularExpressions.Regex.Replace(split[0], @"\s+|\t+", " ");//change many spaces to one
                    var pair = split[0].Split(' ');
                    switch (pair[1].ToLower())
                    {
                        case "numeric":
                            attributes.Add(new ArffAttributeNumeric(attributeIndex, pair[0]));
                            break;
                        case "string"://string
                            attributes.Add(new ArffAttributeString(attributeIndex, pair[0]));
                            break;
                        case "date"://date
                            attributes.Add(new ArffAttributeDate(attributeIndex, pair[0]));
                            break;
                        default:
                            throw new FormatException("The attribute type is unknown");

                    }
                }
                else if (split.Length == 3)
                {
                    var name = split[0].Trim().Unescape();
                    var valueList = split[1];
                    var csv = new CsvReader(new StringReader(valueList), false, trimmingOptions: ValueTrimmingOptions.All);
                    int fieldCount = csv.FieldCount;
                    var values = new string[fieldCount];

                    csv.ReadNextRecord();

                    for (int i = 0; i < fieldCount; i++)
                    {
                        values[i] = csv[i].Unescape();
                    }
                    attributes.Add(new ArffAttributeNominal(attributeIndex, name, values));
                }
                attributeIndex++;
            }

            this.Attributes = new ReadOnlyCollection<ArffAttribute>(attributes);
            this.attributeCount = attributes.Count;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (stream != null)
            {
                stream.Dispose();
            }
        }
    }
}
