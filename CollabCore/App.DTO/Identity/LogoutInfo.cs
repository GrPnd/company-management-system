namespace App.DTO.Identity;

public class LogoutInfo
{
    public string RefreshToken { get; set; } = Guid.NewGuid().ToString();
}