using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MessageRepository : BaseRepository<App.DAL.DTO.Message, App.Domain.Message>, IMessageRepository
{
    public MessageRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new MessageUOWMapper())
    {
    }
}