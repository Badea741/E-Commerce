using FluentValidation;
namespace ECommerce;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        // RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Description).MaximumLength(5000).WithMessage("Description cannot exceed 5000 characters");

    }
}