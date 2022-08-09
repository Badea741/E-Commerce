namespace ECommerce;
public class UserUnitOfWork : BaseUnitOfWork<User>
{
    public UserUnitOfWork(BaseRepo<User> repo) : base(repo)
    {
    }
    public override async Task<User> CreateAsync(User user)
    {
        user.PasswordHash = user.PasswordHash.ToSHA256();
        return await _repo.AddAsync(user);
    }
}