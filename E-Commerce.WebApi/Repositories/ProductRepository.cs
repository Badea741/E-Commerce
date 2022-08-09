namespace ECommerce
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Expressions;

    public class ProductRepository : BaseRepo<Product>
    {
  
        public ProductRepository(ApplicationDbContext dbContext):base(dbContext)
        {
        }
    }
}