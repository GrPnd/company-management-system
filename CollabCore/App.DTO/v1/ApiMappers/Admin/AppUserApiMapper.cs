namespace App.DTO.v1.ApiMappers.Admin;

public class AppUserApiMapper
{
    public static App.DTO.v1.ApiEntities.Admin.AppUser ToDto(App.Domain.Identity.AppUser entity)
    {
        return new App.DTO.v1.ApiEntities.Admin.AppUser
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email
        };
    }

    public static App.Domain.Identity.AppUser ToDomain(App.DTO.v1.ApiEntities.Admin.AppUser dto)
    {
        return new App.Domain.Identity.AppUser
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.Email, // optional: Identity requires UserName
            NormalizedUserName = dto.Email.ToUpperInvariant(), // optional
            NormalizedEmail = dto.Email.ToUpperInvariant()     // optional
        };
    }
}