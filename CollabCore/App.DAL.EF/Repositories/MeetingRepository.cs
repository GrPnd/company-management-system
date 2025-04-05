using App.DAL.Contracts.Repositories;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MeetingRepository : BaseRepository<Meeting>, IMeetingRepository
{
    public MeetingRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}