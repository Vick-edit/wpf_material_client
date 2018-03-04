using System.Collections.Generic;

namespace WPF_client.DomainServices.JsonDataSerialization
{
    /// <summary> Десериализовать список объектов из JSON </summary>
    public interface IJsonDeserializer<OutputType> where OutputType : class
    {
        /// <summary> Десериализовать </summary>
        IList<OutputType> Deserialize(string jsonString);
    }
}