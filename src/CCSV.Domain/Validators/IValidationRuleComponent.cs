namespace CCSV.Domain.Validators;

public interface IValidationRuleComponent<in U>
{
    public string ErrorMessage { get; }
    bool Validate(U value);
}
