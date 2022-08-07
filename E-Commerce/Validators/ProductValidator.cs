namespace ECommerce
{
    using FluentValidation;

    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(p => p.Name).MaximumLength(20).WithMessage("Name max Length is 20");

        }
    }
}
