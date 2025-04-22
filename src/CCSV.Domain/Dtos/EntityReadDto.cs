namespace CCSV.Domain.Dtos;

public abstract class EntityReadDto : EntityDto
{
    public Guid? Id { get; init; }
    public string? EntityCreationDate { get; init; }
    public string? EntityEditionDate { get; init; }
    public string? EntityDisabledDate { get; init; }
    public bool? IsDisabled { get; init; }

}
