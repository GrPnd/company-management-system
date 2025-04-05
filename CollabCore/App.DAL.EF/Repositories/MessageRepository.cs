using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MessageRepository : BaseRepository<Message>, IMessageRepository
{
    public MessageRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}