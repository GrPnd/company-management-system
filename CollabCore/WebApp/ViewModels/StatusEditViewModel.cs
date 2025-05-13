using System.ComponentModel.DataAnnotations;
using Base.Contracts;
using Base.Domain;

namespace WebApp.ViewModels;

public class StatusEditViewModel : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
}