namespace ECommerce;
public class AdminUnitOfWork : BaseUnitOfWork<Admin>
{
    public AdminUnitOfWork(BaseRepo<Admin> repo) : base(repo)
    {
    }
    public override async Task<Admin> CreateAsync(Admin admin)
    {
        admin.PasswordHash = admin.PasswordHash.ToSHA256();
        return await _repo.AddAsync(admin);
    }
}