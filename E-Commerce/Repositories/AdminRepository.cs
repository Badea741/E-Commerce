namespace ECommerce;
public class AdminRepository : BaseRepo<Admin>
{
    public AdminRepository(ApplicationDbContext db) : base(db)
    {
    }
}