namespace CCSV.Domain.Validators;

public interface IValidationRule<in T>
{
    string ErrorMessage { get; }

    ValidationRuleResult Validate(T instance);
}