namespace ArffSharp
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Converts an ARFF file into a compact binary representation with
    /// one byte per value representing the index of the attribute's nominal value.
    /// The binary file only contains the raw values;
    /// attribute information must still be read from the Arff.
    /// </summary>
    public class NominalArffToBin
    {
        /// <summary>
        /// Converts the specified input arff file into its binary representation.
        /// </summary>
        /// <param name="inputArffFile">The input arff file.</param>
        /// <param name="outputFile">The output file.</param>
        public static void Convert(string inputArffFile, string outputFile)
        {
            int lineCount = 0;
            using (ArffReader reader = new ArffReader(inputArffFile))
            {
                using (var writer = new BinaryWriter(File.Open(outputFile, FileMode.Create)))
                {
                    var maxAttributeValueCount = reader.Attributes.Where(attribute => attribute is ArffAttributes.ArffAttributeNominal).Max(a => ((ArffAttributes.ArffAttributeNominal)a).NominalValues.Count);
                    Console.WriteLine("Max attribute value count: " + maxAttributeValueCount);
                    if (maxAttributeValueCount > byte.MaxValue)
                    {
                        throw new ArgumentOutOfRangeException("maxAttributeValueCount", "One attribute has more than " + byte.MaxValue + " possible values.");
                    }

                    writer.Write(reader.Attributes.Count);
                    IArffRecord record;
                    while ((record = reader.ReadNextRecord()) != null)
                    {
                        IArffValue[] values = record.GetValues();
                        for (int i = 0; i < values.Length; i++)
                        {
                            writer.Write((byte)values[i].ValueObj);
                        }
                        ++lineCount;
                        if (lineCount % 1000 == 0 && lineCount > 0) Console.Write(lineCount + " ");
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Total records: " + lineCount);
        }

        /// <summary>
        /// Reads the binary arff data file, returning one byte array per record.
        /// The index in the byte array matches the index of the arff attribute,
        /// and the value of the byte is the index of the attributes nominal values.
        /// If the value of the byte is byte.MaxValue (255), the attribute is missing.
        /// </summary>
        /// <param name="binaryArffData">The binary arff data.</param>
        /// <returns>
        /// The records.
        /// </returns>
        public static IEnumerable<Record> ReadBin(string binaryArffData)
        {
            using (var reader = new BinaryReader(File.OpenRead(binaryArffData)))
            {
                int attributeCount = reader.ReadInt32();

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    var record = new Record();
                    
                    record.Values = reader.ReadBytes(attributeCount - 1);

                    record.Class = reader.ReadByte() == 0;

                    yield return record;
                }
            }
        }

        public class Record
        {
            public byte[] Values { get; set; }
            public bool Class { get; set; }
        }
    }
}
