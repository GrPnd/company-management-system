using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Status : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    public ICollection<Task>? Tasks { get; set; }
}