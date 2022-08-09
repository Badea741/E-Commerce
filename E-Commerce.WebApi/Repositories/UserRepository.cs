namespace ECommerce;
public class UserRepository : BaseRepo<User>
{
    public UserRepository(ApplicationDbContext db) : base(db)
    {
    }
}