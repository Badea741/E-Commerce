using System.Linq.Expressions;

namespace ECommerce;
public abstract class BaseUnitOfWork<TEntity> :IDisposable where TEntity:class{
    protected readonly BaseRepo<TEntity> _repo;
    
    public BaseUnitOfWork(BaseRepo<TEntity> repo){
        _repo=repo;
    }
    public virtual async Task<List<TEntity>> ReadAllAsync(){
        return await _repo.GetAllAsync();
    }
    public virtual async Task<TEntity> CreateAsync(TEntity entity){
        return await _repo.AddAsync(entity);
    }
    public virtual async Task<TEntity> DeleteAsync(Expression<Func<TEntity,bool>> predicate){
        return await _repo.RemoveAsync(predicate);
    }
    public virtual async Task<IEnumerable<TEntity>> DeleteRangeAsync(Expression<Func<TEntity,bool>> predicate){
        return await _repo.RemoveRangeAsync(predicate);
    }
    public virtual async Task<TEntity> ReadByIdAsync(object id){
        return await _repo.GetByIdAsync(id);
    }
    public virtual async Task<TEntity> DeleteByIdAsync(object id){
        return await _repo.RemoveByIdAsync(id);
    }
    public virtual async Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> predicate=null,
                                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy=null,
                                                        string include="")
    {
        return await _repo.Get(predicate,orderBy,include);
    }
    public virtual TEntity Update(TEntity entity){
        return _repo.Edit(entity);
    }
    public virtual async Task<string> SaveAsync(){
        try{
            await _repo.Save();
            return "Done";
        }
        catch(Exception ex){
            return ex.Message;
        }
    }

    public void Dispose()
    {
        _repo.Dispose();
    }
}