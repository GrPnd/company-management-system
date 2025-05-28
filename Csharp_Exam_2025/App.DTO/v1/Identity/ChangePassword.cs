namespace App.DTO.v1.Identity;

public class ChangePassword
{
    public string CurrentPassword { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}