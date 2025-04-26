using App.DAL.Contracts.Repositories;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MeetingRepository : BaseRepository<App.DAL.DTO.Meeting, App.Domain.Meeting>, IMeetingRepository
{
    public MeetingRepository(DbContext repositoryDbContext) : base(repositoryDbContext, new MeetingUOWMapper())
    {
    }
}