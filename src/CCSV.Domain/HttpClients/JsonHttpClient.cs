using CCSV.Domain.Exceptions;
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Web;

namespace CCSV.Domain.HttpClients;

public class JsonHttpClient : IJsonHttpClient
{
    private readonly HttpClient _httpClient;

    public JsonHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public static string ToQuery(object input)
    {
        IEnumerable<string> properties = input.GetType().GetProperties()
            .Where(prop => prop.GetValue(input, null) != null)
            .Select(prop => PropertyToString(input, prop));

        return string.Join("&", properties.ToArray());
    }

    private static string PropertyToString(object input, PropertyInfo property)
    {
        return property.Name + "=" + HttpUtility.UrlEncode(property.GetValue(input, null)!.ToString());
    }

    public static string ToQuery<T>(T input) where T : class
    {
        IEnumerable<string> properties = typeof(T).GetProperties()
            .Where(prop => prop.GetValue(input, null) != null)
            .Select(prop => PropertyToString(input, prop));

        return string.Join("&", properties.ToArray());
    }

    private static string PropertyToString<T>(T input, PropertyInfo property)
    {
        return property.Name + "=" + HttpUtility.UrlEncode(property.GetValue(input, null)!.ToString());
    }

    public virtual async Task<T> Get<T>(string uri)
    {
        T? result = await GetOrDefault<T>(uri);

        if (result is null)
        {
            throw new BusinessException($"The resource ({uri}) does not exist.");
        }

        return result;
    }

    public virtual async Task<T> Get<T>(string uri, IDictionary<string, string?> headers)
    {
        T? result = await GetOrDefault<T>(uri, headers);

        if (result is null)
        {
            throw new BusinessException($"The resource ({uri}) does not exist.");
        }

        return result;
    }

    public virtual async Task<T?> GetOrDefault<T>(string uri)
    {
        using HttpResponseMessage response = await _httpClient.GetAsync(uri);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return default;
        }

        if (!response.IsSuccessStatusCode)
        {
            await HandleError(uri, response);
        }

        T? result = await response.Content.ReadFromJsonAsync<T>();
        return result ?? throw new BusinessException($"It is not possible to parse the body to the ({nameof(T)}) type");
    }

    public virtual async Task<T?> GetOrDefault<T>(string uri, IDictionary<string, string?> headers)
    {
        using HttpRequestMessage request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(uri),
        };

        foreach (KeyValuePair<string, string?> header in headers)
        {
            request.Headers.Add(header.Key, header.Value);
        }

        using HttpResponseMessage response = await _httpClient.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return default;
        }

        if (!response.IsSuccessStatusCode)
        {
            await HandleError(uri, response);
        }

        T? result = await response.Content.ReadFromJsonAsync<T>();
        return result ?? throw new BusinessException($"It is not possible to parse the body to the ({nameof(T)}) type");
    }

    public virtual async Task Post(string uri)
    {
        StringContent request = new StringContent("", Encoding.UTF8, "text/plain");
        using HttpResponseMessage response = await _httpClient.PostAsync(uri, request);

        if (!response.IsSuccessStatusCode)
        {
            await HandleError(uri, response);
        }
    }

    public virtual async Task Post(string uri, IDictionary<string, string?> headers)
    {
        StringContent request = new StringContent("", Encoding.UTF8, "text/plain");

        foreach (KeyValuePair<string, string?> header in headers)
        {
            request.Headers.Add(header.Key, header.Value);
        }

        using HttpResponseMessage response = await _httpClient.PostAsync(uri, request);

        if (!response.IsSuccessStatusCode)
        {
            await HandleError(uri, response);
        }
    }

    public virtual async Task Post<T>(string uri, T value)
    {
        string json = JsonSerializer.Serialize(value);
        StringContent request = new StringContent(json, Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await _httpClient.PostAsync(uri, request);

        if (!response.IsSuccessStatusCode)
        {
            await HandleError(uri, response);
        }
    }

    public virtual async Task Post<T>(string uri, T value, IDictionary<string, string?> headers)
    {
        string json = JsonSerializer.Serialize(value);
        StringContent request = new StringContent(json, Encoding.UTF8, "application/json");

        foreach (KeyValuePair<string, string?> header in headers)
        {
            request.Headers.Add(header.Key, header.Value);
        }

        using HttpResponseMessage response = await _httpClient.PostAsync(uri, request);

        if (!response.IsSuccessStatusCode)
        {
            await HandleError(uri, response);
        }
    }

    public virtual async Task Put(string uri)
    {
        StringContent request = new StringContent("", Encoding.UTF8, "text/plain");
        using HttpResponseMessage response = await _httpClient.PutAsync(uri, request);

        if (!response.IsSuccessStatusCode)
        {
            await HandleError(uri, response);
        }
    }

    public virtual async Task Put(string uri, IDictionary<string, string?> headers)
    {
        StringContent request = new StringContent("", Encoding.UTF8, "text/plain");

        foreach (KeyValuePair<string, string?> header in headers)
        {
            request.Headers.Add(header.Key, header.Value);
        }

        using HttpResponseMessage response = await _httpClient.PutAsync(uri, request);

        if (!response.IsSuccessStatusCode)
        {
            await HandleError(uri, response);
        }
    }

    public virtual async Task Put<T>(string uri, T value)
    {
        string json = JsonSerializer.Serialize(value);
        StringContent request = new StringContent(json, Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await _httpClient.PutAsync(uri, request);

        if (!response.IsSuccessStatusCode)
        {
            await HandleError(uri, response);
        }
    }

    public virtual async Task Put<T>(string uri, T value, IDictionary<string, string?> headers)
    {
        string json = JsonSerializer.Serialize(value);
        StringContent request = new StringContent(json, Encoding.UTF8, "application/json");

        foreach (KeyValuePair<string, string?> header in headers)
        {
            request.Headers.Add(header.Key, header.Value);
        }

        using HttpResponseMessage response = await _httpClient.PutAsync(uri, request);

        if (!response.IsSuccessStatusCode)
        {
            await HandleError(uri, response);
        }
    }

    public virtual async Task Delete(string uri)
    {
        using HttpResponseMessage response = await _httpClient.DeleteAsync(uri);

        if (!response.IsSuccessStatusCode)
        {
            await HandleError(uri, response);
        }
    }

    public virtual async Task Delete(string uri, IDictionary<string, string?> headers)
    {
        using HttpRequestMessage request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(uri),
        };

        foreach (KeyValuePair<string, string?> header in headers)
        {
            request.Headers.Add(header.Key, header.Value);
        }

        using HttpResponseMessage response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            await HandleError(uri, response);
        }
    }

    private async Task HandleError(string uri, HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new BusinessException($"The resource ({uri}) does not exist.");
        }

        ProblemDetails problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>()
            ?? new ProblemDetails() { Status = (int)response.StatusCode, Title = response.StatusCode.ToString() };

        if (problemDetails.Status >= 400 && problemDetails.Status < 500)
        {
            throw new BusinessException($"Http client error ({problemDetails.Status}) | Title: {problemDetails.Title} | Detail: {problemDetails.Detail}");
        }

        if (problemDetails.Status >= 500)
        {
            throw new BadGatewayException($"Http client error ({problemDetails.Status}) | Title: {problemDetails.Title} | Detail: {problemDetails.Detail}");
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _httpClient.Dispose();
        }
    }
}
