using Base.Domain;

namespace App.Domain;

public class WorkDay : BaseEntity
{
    public DateTime Date { get; set; }

    public ICollection<UserInWorkDay> UsersInWorkDay { get; set; } = default!;
}