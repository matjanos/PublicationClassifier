namespace ArffSharp
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using ArffSharp.Extensions;
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
        public ArffRecord ReadNextRecord()
        {
            if (!csvReader.ReadNextRecord()) return null;

            ArffRecord record = new ArffRecord();
            record.Values = new ArffValue[this.attributeCount];

            for (int i = 0; i < this.attributeCount; i++)
            {
                var arffVal = record.Values[i] = new ArffValue();
                var val = csvReader[i].Unescape();
                if (val == "?")
                {
                    arffVal.NominalValueIndex = -1;
                }
                else
                {
                    arffVal.NominalValueIndex = this.Attributes[i].NominalValues.IndexOf(val);

                    if (arffVal.NominalValueIndex == -1)
                    {
                        throw new ArffReaderException("Unknown nominal value \"" + val + "\" for attribute \"" + this.Attributes[i].Name + "\".");
                    }
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

                attributes.Add(new ArffAttribute(attributeIndex, name, values));
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
