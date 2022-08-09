using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce;
public abstract class PersonConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Person
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Age).IsRequired();
        builder.HasIndex(p => p.Username).IsUnique();
        builder.Property(p => p.Username).IsRequired();
        builder.Property(p => p.PhoneNumber).IsRequired();
        builder.Property(p => p.PasswordHash).IsRequired();
        if (typeof(TEntity) == typeof(ECommerce.Admin))
            builder.HasData(
            new Admin
            {
                Id = Guid.NewGuid(),
                Address = "Address",
                Age = 22,
                Name = "Muhammad",
                Username = "admin",
                PasswordHash = "string".ToSHA256(),
                PhoneNumber = "2351234512"
            });
        else
        {
            builder.HasData(
            new User
            {
                Id = Guid.NewGuid(),
                Address = "Address",
                Age = 22,
                Name = "Muhammad",
                Username = "user",
                PasswordHash = "string".ToSHA256(),
                PhoneNumber = "2351234512"
            });
        }
        
    }
}

