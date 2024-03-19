namespace CCS.Domain.Repositories;

public interface ITransaction
{
    Task<TResult> Run<TResult>(Func<Task<TResult>> func);
    Task Run(Func<Task> action);
    TResult Run<TResult>(Func<TResult> func);
    void Run(Action action);
}
