using CCSV.Domain.Dtos;
using CCSV.Domain.HttpClients;

namespace CCSV.Domain.Providers;

public abstract class RestProvider<TRead, TCreate, TUpdate, TQuery, TFilter> : IRestProvider<TRead, TCreate, TUpdate, TQuery, TFilter>
    where TRead : EntityReadDto
    where TCreate : EntityCreateDto
    where TUpdate : EntityUpdateDto
    where TQuery : EntityQueryDto
    where TFilter : EntityFilterDto
{
    private const string IdempotencyKey = "Idempotency-Key";
    private readonly IJsonHttpClient _httpClient;

    protected RestProvider(IJsonHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public abstract string ResourceUri { get; }

    public Task<IEnumerable<TQuery>> GetAll(TFilter filter)
    {
        return _httpClient.Get<IEnumerable<TQuery>>($"{ResourceUri}?{JsonHttpClient.ToQuery(filter)}");
    }

    public Task<IEnumerable<TQuery>> GetAll(TFilter filter, Guid idempotencyKey)
    {
        IDictionary<string, string?> headers = new Dictionary<string, string?>()
        {
            { IdempotencyKey, idempotencyKey.ToString() }
        };

        return _httpClient.Get<IEnumerable<TQuery>>($"{ResourceUri}?{JsonHttpClient.ToQuery(filter)}", headers);
    }

    public Task<int> GetLength(TFilter filter)
    {
        return _httpClient.Get<int>($"{ResourceUri}/Length?{JsonHttpClient.ToQuery(filter)}");
    }

    public Task<int> GetLength(TFilter filter, Guid idempotencyKey)
    {
        IDictionary<string, string?> headers = new Dictionary<string, string?>()
        {
            { IdempotencyKey, idempotencyKey.ToString() }
        };

        return _httpClient.Get<int>($"{ResourceUri}/Length?{JsonHttpClient.ToQuery(filter)}", headers);
    }

    public Task<TRead> GetById(Guid id)
    {
        return _httpClient.Get<TRead>($"{ResourceUri}/{id}");
    }

    public Task<TRead> GetById(Guid id, Guid idempotencyKey)
    {
        IDictionary<string, string?> headers = new Dictionary<string, string?>()
        {
            { IdempotencyKey, idempotencyKey.ToString() }
        };

        return _httpClient.Get<TRead>($"{ResourceUri}/{id}", headers);
    }

    public Task Post(TCreate data)
    {
        return _httpClient.Post(ResourceUri, data);
    }

    public Task Post(TCreate data, Guid idempotencyKey)
    {
        IDictionary<string, string?> headers = new Dictionary<string, string?>()
        {
            { IdempotencyKey, idempotencyKey.ToString() }
        };

        return _httpClient.Post(ResourceUri, data, headers);
    }

    public Task Put(Guid id, TUpdate data)
    {
        return _httpClient.Put($"{ResourceUri}/{id}", data);
    }

    public Task Put(Guid id, TUpdate data, Guid idempotencyKey)
    {
        IDictionary<string, string?> headers = new Dictionary<string, string?>()
        {
            { IdempotencyKey, idempotencyKey.ToString() }
        };

        return _httpClient.Put($"{ResourceUri}/{id}", data, headers);
    }

    public Task Delete(Guid id)
    {
        return _httpClient.Delete($"{ResourceUri}/{id}");
    }

    public Task Delete(Guid id, Guid idempotencyKey)
    {
        IDictionary<string, string?> headers = new Dictionary<string, string?>()
        {
            { IdempotencyKey, idempotencyKey.ToString() }
        };

        return _httpClient.Delete($"{ResourceUri}/{id}", headers);
    }
}
