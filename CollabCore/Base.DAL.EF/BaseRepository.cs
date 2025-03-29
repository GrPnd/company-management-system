using Base.Contracts;
using Base.DAL.Contracts;
using Base.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseRepository<TEntity> : BaseRepository<TEntity, Guid>, IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    public BaseRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}

public class BaseRepository<TEntity, TKey>: IBaseRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    protected DbContext RepositoryDbContext;
    protected DbSet<TEntity> RepositoryDbSet;

    public BaseRepository(DbContext repositoryDbContext)
    {
        RepositoryDbContext = repositoryDbContext;
        RepositoryDbSet = RepositoryDbContext.Set<TEntity>();
    }
    
    
    private IQueryable<TEntity> GetQuery(TKey? id = default!)
    {
        var query = RepositoryDbSet.AsQueryable();
        
        // todo check userid for null/default
        if (typeof(IDomainUser<TKey, IdentityUser<TKey>>).IsAssignableFrom(typeof(TEntity)))
        {
            query = query.Where(e => ((IDomainUser<TKey, IdentityUser<TKey>>)e).UserId.Equals(id));
        }
        return query;
    }
    
    
    public IEnumerable<TEntity> All(TKey? userId = default!)
    {
        return GetQuery(userId).ToList();
    }

    public async Task<IEnumerable<TEntity>> AllAsync(TKey? userId = default!)
    {
        return await GetQuery(userId).ToListAsync();
    }

    public TEntity? Find(TKey id, TKey? userId)
    {
        var query = GetQuery(userId);
        return query.FirstOrDefault(e => e.Id.Equals(id));
    }

    public async Task<TEntity?> FindAsync(TKey id, TKey? userId)
    {
        var query = GetQuery(userId);
        return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public void Add(TEntity entity)
    {
        RepositoryDbSet.Add(entity);
    }

    public TEntity Update(TEntity entity)
    {
        return RepositoryDbSet.Update(entity).Entity;
    }

   public virtual void Remove(TEntity entity, TKey? userId = default!)
    {
        Remove(entity.Id, userId);
    }


    public void Remove(TKey id, TKey? userId)
    {
        var query = GetQuery(userId);
        query = query.Where(e => e.Id.Equals(id));
        var dbEntity = query.FirstOrDefault();
        if (dbEntity != null)
        {
            RepositoryDbSet.Remove(dbEntity);
        }

    }
    public virtual async Task RemoveAsync(TKey id, TKey? userId = default!)
    {
        var query = GetQuery(userId);
        query = query.Where(e => e.Id.Equals(id));
        var dbEntity = await query.FirstOrDefaultAsync();
        if (dbEntity != null)
        {
            RepositoryDbSet.Remove(dbEntity);
        }
    }

    public virtual bool Exists(Guid id, TKey? userId = default)
    {
        var query = GetQuery(userId);
        return query.Any(e => e.Id.Equals(id));
    }

    public virtual async Task<bool> ExistsAsync(Guid id, TKey? userId = default)
    {
        var query = GetQuery(userId);
        return await query.AnyAsync(e => e.Id.Equals(id));
    }

}