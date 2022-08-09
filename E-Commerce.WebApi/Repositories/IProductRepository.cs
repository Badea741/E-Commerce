namespace ECommerce
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IProductRepository
    {
        ApplicationDbContext DbContext { get; }

        Task<Product> AddAsync(Product product);
        Task<Product> DeleteAsync(Guid id);
        Task<Product> EditAsync(Product product);
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetByExprissionAsync(Expression<Func<Product, bool>> expression);
        Task<Product> GetByIdAsync(Guid id);
    }
}