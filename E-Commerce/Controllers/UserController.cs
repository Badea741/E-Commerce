using AutoMapper;
using FluentValidation;

namespace ECommerce;
public class UserController : BaseController<User, UserViewModel>
{
    public UserController(BaseUnitOfWork<User> unitOfWork,
                          IMapper mapper,
                          AbstractValidator<User> validator) : base(unitOfWork, mapper, validator)
    {
    }
}