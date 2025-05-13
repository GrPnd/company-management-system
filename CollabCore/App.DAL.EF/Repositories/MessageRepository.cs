using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Message = App.DAL.DTO.Message;

namespace App.DAL.EF.Repositories;

public class MessageRepository : BaseRepository<App.DAL.DTO.Message, App.Domain.Message>, IMessageRepository
{
    public MessageRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new MessageUOWMapper())
    {
    }


    public async Task<IEnumerable<App.DAL.DTO.Message>> GetMessagesByPersonIdAsync(Guid personId)
    {
        return await RepositoryDbSet
            .Where(m => (m.FromUserId == personId || m.ToUserId == personId))
            .Select(m => UOWMapper.Map(m)!)
            .ToListAsync();
    }
}