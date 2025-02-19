using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ECommerce;
public abstract class BaseRepo<TEntity> : IDisposable where TEntity : class
{
    private readonly ApplicationDbContext db;
    protected readonly DbSet<TEntity> table;

    public BaseRepo(ApplicationDbContext db)
    {
        this.db = db;
        this.table = db.Set<TEntity>();
    }
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await table.AddAsync(entity);
        
        return entity;
    }
    public virtual TEntity Remove(TEntity entity)
    {
        table.Remove(entity);
        return entity;
    }
    public virtual async Task<IEnumerable<TEntity>> RemoveRangeAsync(Expression<Func<TEntity, bool>> predicate)
    {
        IEnumerable<TEntity> entities = (await Get(predicate));
        table.RemoveRange(entities);
        return entities;
    }
    public virtual async Task<TEntity> RemoveAsync(Expression<Func<TEntity, bool>> predicate)
    {
        TEntity entityToRemove = (await Get(predicate)).FirstOrDefault();
        table.Remove(entityToRemove);
        return entityToRemove;
    }
    public virtual async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate = null,
                                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                        string include = ""
                                                        )
    {
        IQueryable<TEntity> query = table;
        if (predicate != null)
            query = table.Where<TEntity>(predicate);

        if (orderBy != null)
            query = orderBy(query);

        string[] includes = include.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (string property in includes)
        {
            query = query.Include(property);
        }

        return await query.ToListAsync();
    }
    public virtual async Task<TEntity> GetByIdAsync(object id)
    {
        return await table.FindAsync(id);
    }
    public virtual async Task<TEntity> RemoveByIdAsync(object id)
    {
        TEntity entityToRemove = await GetByIdAsync(id);
        table.Remove(entityToRemove);
        return entityToRemove;
    }
    public virtual TEntity Edit(TEntity entity)
    {
        table.Update(entity);
        return entity;
    }
    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await table.ToListAsync();
    }
    public virtual async Task Save()
    {
        await db.SaveChangesAsync();
    }

    public void Dispose()
    {
        db.Dispose();
    }
}