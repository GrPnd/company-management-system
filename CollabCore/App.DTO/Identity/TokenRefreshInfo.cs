namespace App.DTO.Identity;

public class TokenRefreshInfo
{
    public string JWT { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}