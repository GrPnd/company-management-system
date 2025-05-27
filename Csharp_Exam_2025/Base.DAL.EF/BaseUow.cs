using Base.DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseUow<TDbContext> : IBaseUOW
    where TDbContext : DbContext
{
    protected readonly TDbContext UowDbContext;

    public BaseUow(TDbContext uowDbContext)
    {
        UowDbContext = uowDbContext;
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await UowDbContext.SaveChangesAsync();
    }
}