namespace CCSV.Domain.Mappers;

public interface IMasterMapper
{
    TDestiny Map<TOrigin, TDestiny>(TOrigin origin);
}
