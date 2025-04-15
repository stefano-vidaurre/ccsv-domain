namespace CCSV.Domain.Dtos;

public abstract class EntityFilterDto : EntityDto
{
    public string? EntityCreationDateGreaterThan { get; init; } = null;
    public string? EntityCreationDateLessThan { get; init; } = null;
    public bool DisabledIncluded { get; init; } = false;
    public int Index { get; init; } = 0;
    public int Size { get; init; } = 100;
}
