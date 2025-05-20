using CCSV.Domain.Dtos;

namespace CCSV.Domain.Providers;

public interface IRestProvider<TRead, TCreate, TUpdate, TQuery, TFilter> : IRestProvider<TRead, TCreate, TQuery, TFilter>
    where TRead : EntityReadDto
    where TCreate : EntityCreateDto
    where TUpdate : EntityUpdateDto
    where TQuery : EntityQueryDto
    where TFilter : EntityFilterDto
{
    Task Update(Guid id, TUpdate data);
    Task Update(Guid id, TUpdate data, Guid idempotencyKey);
}

public interface IRestProvider<TRead, TCreate, TQuery, TFilter>
    where TRead : EntityReadDto
    where TCreate : EntityCreateDto
    where TQuery : EntityQueryDto
    where TFilter : EntityFilterDto
{
    string ResourceUri { get; }
    Task<IEnumerable<TQuery>> GetAll(TFilter filter);
    Task<IEnumerable<TQuery>> GetAll(TFilter filter, Guid idempotencyKey);
    Task<int> GetLength(TFilter filter);
    Task<int> GetLength(TFilter filter, Guid idempotencyKey);
    Task<TRead> GetById(Guid id);
    Task<TRead> GetById(Guid id, Guid idempotencyKey);
    Task<TRead?> GetByIdOrDefault(Guid id);
    Task<TRead?> GetByIdOrDefault(Guid id, Guid idempotencyKey);
    Task Create(TCreate data);
    Task Create(TCreate data, Guid idempotencyKey);
    Task Delete(Guid id);
    Task Delete(Guid id, Guid idempotencyKey);
    Task Enable(Guid id);
    Task Enable(Guid id, Guid idempotencyKey);
    Task Disable(Guid id);
    Task Disable(Guid id, Guid idempotencyKey);
}