namespace CCSV.Domain.HttpClients;

internal class ProblemDetails
{
    public int Status { get; init; } = 0;
    public string Title { get; init; } = string.Empty;
    public string Detail { get; init; } = string.Empty;
}
