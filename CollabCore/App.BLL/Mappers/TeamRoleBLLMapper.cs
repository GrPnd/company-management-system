using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class TeamRoleBLLMapper : IBLLMapper<App.BLL.DTO.TeamRole, App.DAL.DTO.TeamRole>
{
    private readonly UserInTeamBLLMapper _userInTeamBLLMapper = new();
    public TeamRole? Map(DTO.TeamRole? entity)
    {
        if (entity == null) return null;

        var res = new TeamRole()
        {
            Id = entity.Id,
            Name = entity.Name,
            UsersInTeams = entity.UsersInTeams?.Select(u => _userInTeamBLLMapper.Map(u)).ToList()!
        };
        
        return res;
    }

    public DTO.TeamRole? Map(TeamRole? entity)
    {
        if (entity == null) return null;

        var res = new DTO.TeamRole()
        {
            Id = entity.Id,
            Name = entity.Name,
            UsersInTeams = entity.UsersInTeams?.Select(u => _userInTeamBLLMapper.Map(u)).ToList()!
        };
        
        return res;
    }
    
    public static TeamRole? MapSimple(DTO.TeamRole? entity)
    {
        if (entity == null) return null;

        return new TeamRole()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
    
    public static DTO.TeamRole? MapSimple(TeamRole? entity)
    {
        if (entity == null) return null;

        return new DTO.TeamRole()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}