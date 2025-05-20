using CCSV.Domain.Dtos;
using CCSV.Domain.HttpClients;

namespace CCSV.Domain.Providers;

public abstract class RestProvider<TRead, TCreate, TUpdate, TQuery, TFilter> : RestProvider<TRead, TCreate, TQuery, TFilter>, IRestProvider<TRead, TCreate, TUpdate, TQuery, TFilter>
    where TRead : EntityReadDto
    where TCreate : EntityCreateDto
    where TUpdate : EntityUpdateDto
    where TQuery : EntityQueryDto
    where TFilter : EntityFilterDto
{
    private const string IdempotencyKey = "Idempotency-Key";
    private readonly IJsonHttpClient _httpClient;

    protected RestProvider(IJsonHttpClient httpClient) : base(httpClient)
    {
        _httpClient = httpClient;
    }

    public virtual Task Update(Guid id, TUpdate data)
    {
        return _httpClient.Put($"{ResourceUri}/{id}", data);
    }

    public virtual Task Update(Guid id, TUpdate data, Guid idempotencyKey)
    {
        IDictionary<string, string?> headers = new Dictionary<string, string?>()
        {
            { IdempotencyKey, idempotencyKey.ToString() }
        };

        return _httpClient.Put($"{ResourceUri}/{id}", data, headers);
    }
}

public abstract class RestProvider<TRead, TCreate, TQuery, TFilter> : IRestProvider<TRead, TCreate, TQuery, TFilter>
    where TRead : EntityReadDto
    where TCreate : EntityCreateDto
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

    public virtual Task<IEnumerable<TQuery>> GetAll(TFilter filter)
    {
        return _httpClient.Get<IEnumerable<TQuery>>($"{ResourceUri}?{JsonHttpClient.ToQuery(filter)}");
    }

    public virtual Task<IEnumerable<TQuery>> GetAll(TFilter filter, Guid idempotencyKey)
    {
        IDictionary<string, string?> headers = new Dictionary<string, string?>()
        {
            { IdempotencyKey, idempotencyKey.ToString() }
        };

        return _httpClient.Get<IEnumerable<TQuery>>($"{ResourceUri}?{JsonHttpClient.ToQuery(filter)}", headers);
    }

    public virtual Task<int> GetLength(TFilter filter)
    {
        return _httpClient.Get<int>($"{ResourceUri}/Length?{JsonHttpClient.ToQuery(filter)}");
    }

    public virtual Task<int> GetLength(TFilter filter, Guid idempotencyKey)
    {
        IDictionary<string, string?> headers = new Dictionary<string, string?>()
        {
            { IdempotencyKey, idempotencyKey.ToString() }
        };

        return _httpClient.Get<int>($"{ResourceUri}/Length?{JsonHttpClient.ToQuery(filter)}", headers);
    }

    public virtual Task<TRead> GetById(Guid id)
    {
        return _httpClient.Get<TRead>($"{ResourceUri}/{id}");
    }

    public virtual Task<TRead> GetById(Guid id, Guid idempotencyKey)
    {
        IDictionary<string, string?> headers = new Dictionary<string, string?>()
        {
            { IdempotencyKey, idempotencyKey.ToString() }
        };

        return _httpClient.Get<TRead>($"{ResourceUri}/{id}", headers);
    }

    public virtual Task<TRead?> GetByIdOrDefault(Guid id)
    {
        return _httpClient.GetOrDefault<TRead>($"{ResourceUri}/{id}");
    }

    public virtual Task<TRead?> GetByIdOrDefault(Guid id, Guid idempotencyKey)
    {
        IDictionary<string, string?> headers = new Dictionary<string, string?>()
        {
            { IdempotencyKey, idempotencyKey.ToString() }
        };

        return _httpClient.GetOrDefault<TRead>($"{ResourceUri}/{id}", headers);
    }

    public virtual Task Create(TCreate data)
    {
        return _httpClient.Post(ResourceUri, data);
    }

    public virtual Task Create(TCreate data, Guid idempotencyKey)
    {
        IDictionary<string, string?> headers = new Dictionary<string, string?>()
        {
            { IdempotencyKey, idempotencyKey.ToString() }
        };

        return _httpClient.Post(ResourceUri, data, headers);
    }

    public virtual Task Delete(Guid id)
    {
        return _httpClient.Delete($"{ResourceUri}/{id}");
    }

    public virtual Task Delete(Guid id, Guid idempotencyKey)
    {
        IDictionary<string, string?> headers = new Dictionary<string, string?>()
        {
            { IdempotencyKey, idempotencyKey.ToString() }
        };

        return _httpClient.Delete($"{ResourceUri}/{id}", headers);
    }

    public virtual Task Enable(Guid id)
    {
        return _httpClient.Put($"{ResourceUri}/{id}/Enabled");
    }

    public virtual Task Enable(Guid id, Guid idempotencyKey)
    {
        IDictionary<string, string?> headers = new Dictionary<string, string?>()
        {
            { IdempotencyKey, idempotencyKey.ToString() }
        };

        return _httpClient.Put($"{ResourceUri}/{id}/Enabled", headers);
    }

    public virtual Task Disable(Guid id)
    {
        return _httpClient.Put($"{ResourceUri}/{id}/Disabled");
    }

    public virtual Task Disable(Guid id, Guid idempotencyKey)
    {
        IDictionary<string, string?> headers = new Dictionary<string, string?>()
        {
            { IdempotencyKey, idempotencyKey.ToString() }
        };

        return _httpClient.Put($"{ResourceUri}/{id}/Disabled", headers);
    }
}
