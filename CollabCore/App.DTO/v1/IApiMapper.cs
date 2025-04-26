using Base.Contracts;

namespace App.DTO.v1;

public interface IApiMapper<TApiEntity, TBllEntity> : IApiMapper<TApiEntity, TBllEntity, Guid>
    where TApiEntity : class, IDomainId
    where TBllEntity : class, IDomainId
{
}

public interface IApiMapper<TApiEntity, TBllEntity, TKey>
    where TKey : IEquatable<TKey>
    where TApiEntity : class, IDomainId<TKey>
    where TBllEntity : class, IDomainId<TKey>
{
    public TApiEntity? Map(TBllEntity? entity);
    public TBllEntity? Map(TApiEntity? entity);
}