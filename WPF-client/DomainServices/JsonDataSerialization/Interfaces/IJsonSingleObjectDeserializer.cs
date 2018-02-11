namespace WPF_client.DomainServices.JsonDataSerialization
{
    public interface IJsonSingleObjectDeserializer<OutputType> where OutputType : class
    {
        OutputType Deserialize(string jsonString);
    }
}