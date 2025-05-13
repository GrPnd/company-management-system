using Base.Contracts;

namespace WebApp.ViewModels;

public class WorkDayEditViewModel : IDomainId
{
    public Guid Id { get; set; }
    
    public string Day { get; set; } = default!;
}