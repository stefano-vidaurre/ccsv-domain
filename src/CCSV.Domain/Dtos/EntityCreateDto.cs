namespace CCSV.Domain.Dtos;

public abstract class EntityCreateDto : EntityDto
{
    public Guid? Id { get; init; }
}
