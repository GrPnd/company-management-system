using Base.Contracts;
using Base.Domain;

namespace WebApp.ViewModels;

public class WorkDayEditViewModel : IDomainId
{
    public Guid Id { get; set; }
    
    public DateTime Day { get; set; }
}