namespace CCSV.Domain.HttpClients;

public interface IJsonHttpClient : IDisposable
{
    Task<T> Get<T>(string uri);
    Task<T> Get<T>(string uri, IDictionary<string, string?> headers);
    Task<T?> GetOrDefault<T>(string uri);
    Task<T?> GetOrDefault<T>(string uri, IDictionary<string, string?> headers);
    Task Post(string uri);
    Task Post(string uri, IDictionary<string, string?> headers);
    Task Post<T>(string uri, T value);
    Task Post<T>(string uri, T value, IDictionary<string, string?> headers);
    Task Put(string uri);
    Task Put(string uri, IDictionary<string, string?> headers);
    Task Put<T>(string uri, T value);
    Task Put<T>(string uri, T value, IDictionary<string, string?> headers);
    Task Delete(string uri);
    Task Delete(string uri, IDictionary<string, string?> headers);
}
