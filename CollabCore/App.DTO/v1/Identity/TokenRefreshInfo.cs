﻿namespace App.DTO.v1.Identity;

public class TokenRefreshInfo
{
    public string JWT { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}