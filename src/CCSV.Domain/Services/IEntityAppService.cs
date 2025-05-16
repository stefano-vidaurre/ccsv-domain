using CCSV.Domain.Dtos;

namespace CCSV.Domain.Services;

public interface IEntityAppService<TRead, TCreate, TUpdate, TQuery, TFilter>
    where TRead : EntityReadDto
    where TCreate : EntityCreateDto
    where TUpdate : EntityUpdateDto
    where TQuery : EntityQueryDto
    where TFilter : EntityFilterDto
{
    Task<IEnumerable<TQuery>> GetAll(TFilter filter);
    Task<TRead> GetById(Guid id);
    Task<int> GetLength(TFilter filter);
    Task Create(TCreate data);
    Task Update(Guid id, TUpdate data);
    Task Delete(Guid id);
    Task Enable(Guid id);
    Task Disable(Guid id);
}
