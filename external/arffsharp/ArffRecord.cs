using System;
using System.Collections.ObjectModel;
using ArffSharp.ArffAttributes;
using ArffSharp.Extensions;

namespace ArffSharp
{


    /// <summary>
    /// Represents a single data row in an ARFF file.
    /// </summary>
    public class ArffRecord : IArffRecord
    {
        private readonly ReadOnlyCollection<ArffAttribute> _attributesCollection;

        public ArffRecord(ReadOnlyCollection<ArffAttribute> attributes)
        {
            Values = new IArffValue[attributes.Count];
            _attributesCollection = attributes;
        }

        public IArffValue[] Values { get; private set; }
        protected int Iterator = 0;

        //public int Weight { private get; private set; }//TODO: http://weka.wikispaces.com/ARFF+(stable+version)#Instance weights in ARFF files
        
        public void AddValue(string value)
        {
            IArffValue arffVal;
             switch (_attributesCollection[Iterator].TypeKeyWord)
                {
                    case "numeric":
                        arffVal = new ArffValueNumeric(float.Parse(value),Iterator);
                        break;
                    case "date":
                        var date = ((ArffAttributeDate) _attributesCollection[Iterator]).ConvertStringToDate(value);
                        arffVal = new ArffValueDate(date, Iterator);
                        break;
                    case "string":
                        arffVal = new ArffValueString(value,Iterator);
                        break;
                    case "":
                        var val = value.Unescape();
                        if (val.Equals("?"))
                        {
                            arffVal = new ArffValueNominal(-1,Iterator);
                        }
                        else
                        {
                            arffVal = new ArffValueNominal(((ArffAttributeNominal)_attributesCollection[Iterator]).NominalValues.IndexOf(val),Iterator);

                            if (((ArffValueNominal)arffVal).Value == -1)
                            {
                                throw new ArffReaderException(
                                    "Unknown nominal value \""
                                    + val + "\" for attribute \"" 
                                    + _attributesCollection[Iterator].Name 
                                    + "\".");
                            }
                        }
                        break;
                    default:
                        throw new Exception("Unknown ARFF attribute type");
                }
            Values[Iterator] = arffVal;
            Iterator++;
        }


        IArffValue[] IArffRecord.GetValues()
        {
            return Values;
        }
    }
}
