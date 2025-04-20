using App.Domain;
using App.Domain.Identity;
using Base.Contracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task = App.Domain.Task;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    
    public DbSet<Absence> Absences { get; set; } = default!;
    public DbSet<Department> Departments { get; set; } = default!;
    public DbSet<Meeting> Meetings { get; set; } = default!;
    public DbSet<Message> Messages { get; set; } = default!;
    public DbSet<Role> Roles { get; set; } = default!;
    public DbSet<Schedule> Schedules { get; set; } = default!;
    public DbSet<Status> Statuses { get; set; } = default!;
    public DbSet<Task> Tasks { get; set; } = default!;
    public DbSet<Team> Teams { get; set; } = default!;
    public DbSet<Ticket> Tickets { get; set; } = default!;
    public DbSet<Person> Persons { get; set; } = default!;
    public DbSet<UserInRole> UsersInRoles { get; set; } = default!;
    public DbSet<UserInTeam> UsersInTeams { get; set; } = default!;
    public DbSet<UserInTeamInTask> UsersInTeamsInTasks { get; set; } = default!;
    public DbSet<UserInWorkDay> UsersInWorkDays { get; set; } = default!;
    public DbSet<WorkDay> WorkDays { get; set; } = default!;
    public DbSet<AppRefreshToken> RefreshTokens { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var addedEntries = ChangeTracker.Entries()
            .Where(e => e is { Entity: IDomainMeta });
        foreach (var entry in addedEntries)
        {
            if (entry.State == EntityState.Added)
            {
                (entry.Entity as IDomainMeta)!.CreatedAt = DateTime.UtcNow;
                (entry.Entity as IDomainMeta)!.CreatedBy = "system";
            }
            else if (entry.State == EntityState.Modified)
            {
                (entry.Entity as IDomainMeta)!.ChangedAt = DateTime.UtcNow;
                (entry.Entity as IDomainMeta)!.ChangedBy = "system";
                
                // Prevent overwriting CreatedBy/CreatedAt/UserId on update
                entry.Property("CreatedAt").IsModified = false;
                entry.Property("CreatedBy").IsModified = false;

                entry.Property("UserId").IsModified = false;
            }
        }


        return base.SaveChangesAsync(cancellationToken);
    }

}