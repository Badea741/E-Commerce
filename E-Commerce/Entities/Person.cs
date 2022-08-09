namespace ECommerce;
public abstract class Person : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public int Age { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}