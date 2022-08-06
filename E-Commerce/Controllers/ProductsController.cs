// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController<Product,ProductViewModel>
    {
        public ProductsController(BaseUnitOfWork<Product> productUnitOfWork,IMapper mapper):base(productUnitOfWork,mapper)
        {
        }
    }
}