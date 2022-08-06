namespace ECommerce
{
    public class ProductUnitOfWork :BaseUnitOfWork<Product>
    {
        private readonly BaseRepo<Product> _productRepsitory;

        public ProductUnitOfWork(BaseRepo<Product> productRepsitory):base(productRepsitory)
        {
            _productRepsitory = productRepsitory;
        }
    }
}