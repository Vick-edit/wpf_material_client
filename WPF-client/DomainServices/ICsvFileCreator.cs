using System.Collections.Generic;

namespace WPF_client.DomainServices
{
    public interface ICsvFileCreator
    {
        bool SaveToFile<T>(string fullFilePath, IList<T> forecasts) where T : class ;
    }
}