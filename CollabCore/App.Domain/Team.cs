using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Team : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; init; } = default!;
    
    public DateTime CreatedAt { get; init; }
    public DateTime? DeletedAt { get; init; }
    
    public ICollection<UserInTeam>? Users { get; init; }
    public ICollection<Schedule>? Schedules { get; init; }
    public ICollection<Meeting>? Meetings { get; init; }
    
    public Guid DepartmentId { get; init; }
    public Department? Department { get; init; }
}