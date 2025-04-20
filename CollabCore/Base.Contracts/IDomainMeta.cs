namespace Base.Contracts;

public interface IDomainMeta
{
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public string? ChangedBy { get; set; }
    public DateTime? ChangedAt { get; set; }
    
    public string? SysNotes { get; set; }
}