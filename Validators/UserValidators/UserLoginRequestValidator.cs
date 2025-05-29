using FluentValidation;
using userJwtApp.Models.UserModel;

namespace userJwtApp.Validators.UserValidators;

public class UserLoginRequestValidator : AbstractValidator<UserLoginRequestModel>
{
    public UserLoginRequestValidator()
    {
        RuleFor(model => model.Username)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(model => model.Password)
            .NotNull()
            .NotEmpty();        
    }
}