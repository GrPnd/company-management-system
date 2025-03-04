using App.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task = App.Domain.Task;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext
{
    
    public DbSet<Absence> Absences { get; set; } = default!;
    public DbSet<Department> Departments { get; set; } = default!;
    public DbSet<Meeting> Meetings { get; set; } = default!;
    public DbSet<Message> Messages { get; set; } = default!;
    // public DbSet<Role> Roles { get; set; } = default!;
    public DbSet<Schedule> Schedules { get; set; } = default!;
    public DbSet<Status> Statuses { get; set; } = default!;
    public DbSet<Task> Tasks { get; set; } = default!;
    public DbSet<Team> Teams { get; set; } = default!;
    public DbSet<Ticket> Tickets { get; set; } = default!;
    // public DbSet<User> Users { get; set; } = default!;
    // public DbSet<UserInRole> UsersInRoles { get; set; } = default!;
    public DbSet<UserInTeam> UsersInTeams { get; set; } = default!;
    public DbSet<UserInTeamInTask> UsersInTeamsInTasks { get; set; } = default!;
    public DbSet<UserInWorkDay> UsersInWorkDays { get; set; } = default!;
    public DbSet<WorkDay> WorkDays { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}