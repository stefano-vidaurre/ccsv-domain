namespace CCSV.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    void UpdateEditDateTimes();
    Task SaveAsync();
    void Save();
    void Clear();
}
