using Base.Domain;

namespace Base.DAL.Contracts;

public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, Guid>
    where TEntity : BaseEntity
{
}

public interface IBaseRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    IEnumerable<TEntity> All(TKey? userId);
    Task<IEnumerable<TEntity>> AllAsync(TKey? userId);

    TEntity? Find(TKey id, TKey? userId);
    Task<TEntity?> FindAsync(TKey id, TKey? userId);

    void Add(TEntity entity);

    TEntity Update(TEntity entity);

    void Remove(TEntity entity, TKey? userId = default!);
    void Remove(TKey id, TKey? userId = default!);
    Task RemoveAsync(TKey id, TKey? userId = default!);
    
    bool Exists(Guid id, TKey? userId = default!);
    Task<bool> ExistsAsync(Guid id, TKey? userId = default!);

}