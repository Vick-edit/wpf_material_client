using System.Collections.Generic;

namespace WPF_client.DomainServices.JsonDataSerialization
{
    public interface IJsonDeserializer<OutputType> where OutputType : class
    {
        IList<OutputType> Deserialize(string jsonString);
    }
}