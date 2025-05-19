using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PersonRepository : BaseRepository<App.DAL.DTO.Person, App.Domain.Person>, IPersonRepository
{
    public PersonRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new PersonUOWMapper())
    {
    }
    
    public async Task<IEnumerable<App.DAL.DTO.Person>> GetAdmins()
    {
        return await RepositoryDbSet
            .Where(p => p.PersonName == "Admin")
            .Select(m => UOWMapper.Map(m)!)
            .ToListAsync();
    }
    
    public async Task<App.DAL.DTO.Person?> FindByUserIdAsync(Guid userId)
    {
        var res = await RepositoryDbSet.FirstOrDefaultAsync(p => p.UserId == userId);
        return UOWMapper.Map(res);
    }
}