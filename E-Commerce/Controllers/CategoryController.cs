using AutoMapper;
using FluentValidation;

namespace ECommerce;
public class CategoryController : BaseController<Category, CategoryViewModel>
{
    public CategoryController(BaseUnitOfWork<Category> unitOfWork,
        IMapper mapper,
        AbstractValidator<CategoryViewModel> validator)
    : base(unitOfWork, mapper, validator)
    {
    }
}