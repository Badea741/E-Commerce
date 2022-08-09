using AutoMapper;

namespace ECommerce;
public class UserAuthController : AuthenticationController<User>
{
    public UserAuthController(IConfiguration configuration,
                              IMapper mapper,
                              BaseUnitOfWork<User> unitOfWork) : base(configuration, mapper, unitOfWork)
    {

    }
}