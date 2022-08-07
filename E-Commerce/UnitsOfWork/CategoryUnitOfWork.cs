namespace ECommerce;
public class CategoryUnitOfWork : BaseUnitOfWork<Category>
{
    public CategoryUnitOfWork(BaseRepo<Category> repo) : base(repo)
    {
    }
}