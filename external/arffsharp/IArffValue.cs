namespace ArffSharp
{
    /// <summary>
    /// Represents a single data value in an ArffRecord.
    /// </summary>
    public interface IArffValue
    {

        int AttributeNo
        {
            get;
        }

        object ValueObj
        {
            get;
        }

    }
}
