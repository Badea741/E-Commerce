namespace ECommerce;
public class CategoryRepository : BaseRepo<Category>
{
    public CategoryRepository(ApplicationDbContext db) : base(db)
    {
    }
}