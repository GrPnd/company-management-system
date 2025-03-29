namespace Base.Contracts;

public interface IBaseUOW
{
    public Task<int> SaveChangesAsync();
}