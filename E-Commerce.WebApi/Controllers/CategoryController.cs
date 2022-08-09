using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce;
[Authorize(Roles = $"User,Admin")]
public class CategoryController : BaseController<Category, CategoryViewModel>
{
    public CategoryController(BaseUnitOfWork<Category> unitOfWork,
        IMapper mapper,
        AbstractValidator<Category> validator)
    : base(unitOfWork, mapper, validator)
    {
    }
}