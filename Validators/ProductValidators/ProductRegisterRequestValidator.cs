using FluentValidation;
using userJwtApp.Models.ProductModels;

namespace userJwtApp.Validators.ProductValidators;

public class ProductRegisterRequestValidator : AbstractValidator<ProductRegisterRequestModel>
{
    public ProductRegisterRequestValidator()
    {
        RuleFor(model => model.ProductName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(model => model.Serial)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(model => model.Price)
            .NotNull()
            .NotEmpty();
    }
}