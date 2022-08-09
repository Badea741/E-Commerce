using AutoMapper;
using FluentValidation;

namespace ECommerce;
public class AdminController : BaseController<Admin, AdminViewModel>
{
    public AdminController(BaseUnitOfWork<Admin> unitOfWork, IMapper mapper, AbstractValidator<Admin> validator) : base(unitOfWork, mapper, validator)
    {
    }
}