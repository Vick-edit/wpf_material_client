namespace WPF_client.DomainServices.JsonDataSerialization
{
    /// <summary> Десериализовать объект из JSON </summary>
    public interface IJsonSingleObjectDeserializer<OutputType> where OutputType : class
    {
        /// <summary> Десериализовать </summary>
        OutputType Deserialize(string jsonString);
    }
}