using AutoMapper;

namespace ECommerce;
public class AdminAuthController : AuthenticationController<Admin>
{
    public AdminAuthController(IConfiguration configuration, IMapper mapper, BaseUnitOfWork<Admin> unitOfWork) : base(configuration, mapper, unitOfWork)
    {
    }
}