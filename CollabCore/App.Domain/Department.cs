using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Department : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; init; } = default!;
}