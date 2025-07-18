using FluentValidation;
using userJwtApp.Models.UserModels;

namespace userJwtApp.Validators.UserValidators;

public class UserSignRequestValidator : AbstractValidator<UserSignRequestModel>
{
    public UserSignRequestValidator()
    {
        RuleFor(model => model.FirstName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(model => model.LastName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(model => model.Username)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(model => model.Password)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo("8");
        RuleFor(model => model.Email)
            .NotNull()
            .EmailAddress()
            .MaximumLength(100);            
    }
}