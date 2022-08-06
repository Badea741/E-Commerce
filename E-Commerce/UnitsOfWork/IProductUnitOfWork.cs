namespace ECommerce
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductUnitOfWork
    {
        Task<Product> CreateAsync(Product product);
        Task<Product> DeleteAsync(Guid productId);
        Task<List<Product>> ReadAsync();
        Task<Product> ReadByIdAsync(Guid productId);
        Task<Product> UpdateAsync(Product product);
    }
}
