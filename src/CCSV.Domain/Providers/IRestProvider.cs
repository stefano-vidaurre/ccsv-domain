using CCSV.Domain.Dtos;

namespace CCSV.Domain.Providers;

public interface IRestProvider<TRead, TCreate, TUpdate, TQuery, TFilter>
    where TRead : EntityReadDto
    where TCreate : EntityCreateDto
    where TUpdate : EntityUpdateDto
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
    Task Post(TCreate data);
    Task Post(TCreate data, Guid idempotencyKey);
    Task Put(Guid id, TUpdate data);
    Task Put(Guid id, TUpdate data, Guid idempotencyKey);
    Task Delete(Guid id);
    Task Delete(Guid id, Guid idempotencyKey);
}
