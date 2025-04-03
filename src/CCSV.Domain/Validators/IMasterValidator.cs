namespace CCSV.Domain.Validators;

public interface IMasterValidator
{
    void Validate<T>(T instance);
}
