using Base.Contracts;
using Base.DAL.Contracts;
using Base.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseRepository<TDalEntity, TDomainEntity> : BaseRepository<TDalEntity, TDomainEntity, Guid>,
    IBaseRepository<TDalEntity>
    where TDalEntity : class, IDomainId
    where TDomainEntity : class, IDomainId
{
    public BaseRepository(DbContext repositoryDbContext, IUOWMapper<TDalEntity, TDomainEntity> iuowMapper)
        : base(repositoryDbContext, iuowMapper)
    {
    }
}

public class BaseRepository<TDalEntity, TDomainEntity, TKey> : IBaseRepository<TDalEntity, TKey>
    where TDalEntity : class, IDomainId<TKey>
    where TDomainEntity : class, IDomainId<TKey>
    where TKey : IEquatable<TKey>
{
    protected DbContext RepositoryDbContext;
    protected DbSet<TDomainEntity> RepositoryDbSet;
    protected IUOWMapper<TDalEntity, TDomainEntity, TKey> IuowMapper;

    public BaseRepository(DbContext repositoryDbContext, IUOWMapper<TDalEntity, TDomainEntity, TKey> iuowMapper)
    {
        RepositoryDbContext = repositoryDbContext;
        IuowMapper = iuowMapper;
        RepositoryDbSet = RepositoryDbContext.Set<TDomainEntity>();
    }


    protected virtual IQueryable<TDomainEntity> GetQuery(TKey? userId = default!)
    {
        var query = RepositoryDbSet.AsQueryable();

        // todo : check userId for null/default
        if (typeof(IDomainUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)) &&
            userId != null &&
            !EqualityComparer<TKey>.Default.Equals(userId, default))
        {
            query = query.Where(e => ((IDomainUserId<TKey>)e).UserId.Equals(userId));
        }

        return query;
    }

    // TODO: Implement UOW, remove SaveChangesAsync from repository
    public async Task<int> SaveChangesAsync()
    {
        return await RepositoryDbContext.SaveChangesAsync();
    }

    public virtual IEnumerable<TDalEntity> All(TKey? userId = default!)
    {
        return GetQuery(userId)
            .ToList()
            .Select(e => IuowMapper.Map(e)!);
    }

    public virtual async Task<IEnumerable<TDalEntity>> AllAsync(TKey? userId = default!)
    {
        return (await GetQuery(userId)
                .ToListAsync())
            .Select(e => IuowMapper.Map(e)!);
    }

    public virtual TDalEntity? Find(TKey id, TKey? userId = default!)
    {
        var query = GetQuery(userId);
        var res = query.FirstOrDefault(e => e.Id.Equals(id));
        return IuowMapper.Map(res);
    }

    public virtual async Task<TDalEntity?> FindAsync(TKey id, TKey? userId = default!)
    {
        var query = GetQuery(userId);
        var res = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        return IuowMapper.Map(res);
    }

    public virtual void Add(TDalEntity entity, TKey? userId = default!)
    {
        var dbEntity = IuowMapper.Map(entity);
        
        if (typeof(IDomainUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)) &&
            userId != null &&
            !EqualityComparer<TKey>.Default.Equals(userId, default))
        {
           ((IDomainUserId<TKey>) dbEntity!).UserId = userId;
        }
        
        RepositoryDbSet.Add(dbEntity!);
    }

    public virtual TDalEntity Update(TDalEntity entity)
    {
        return IuowMapper.Map(RepositoryDbSet.Update(IuowMapper.Map(entity)!).Entity)!;
    }

    public virtual void Remove(TDalEntity entity, TKey? userId = default!)
    {
        Remove(entity.Id, userId);
    }

    public virtual void Remove(TKey id, TKey? userId)
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
