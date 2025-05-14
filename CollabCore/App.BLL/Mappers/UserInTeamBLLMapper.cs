using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class UserInTeamBLLMapper : IBLLMapper<App.BLL.DTO.UserInTeam, App.DAL.DTO.UserInTeam>
{
    private readonly TaskBLLMapper _taskBLLMapper = new();
    private readonly UserInTeamInTaskBLLMapper _userInTeamInTaskBLLMapper = new();
    public UserInTeam? Map(DTO.UserInTeam? entity)
    {
        if (entity == null) return null;

        var res = new UserInTeam()
        {
            Id = entity.Id,
            TeamRoleId = entity.TeamRoleId,
            TeamRole = TeamRoleBLLMapper.MapSimple(entity.TeamRole),
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = PersonBLLMapper.MapSimple(entity.User),
            TeamId = entity.TeamId,
            Team = TeamBLLMapper.MapSimple(entity.Team!),
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => _userInTeamInTaskBLLMapper.Map(u)).ToList()!
        };
        
        return res;
    }

    public DTO.UserInTeam? Map(UserInTeam? entity)
    {
        if (entity == null) return null;

        var res = new DTO.UserInTeam()
        {
            Id = entity.Id,
            TeamRoleId = entity.TeamRoleId,
            TeamRole = TeamRoleBLLMapper.MapSimple(entity.TeamRole),
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            User = PersonBLLMapper.MapSimple(entity.User),
            TeamId = entity.TeamId,
            Team = TeamBLLMapper.MapSimple(entity.Team!),
            UserInTeamInTasks = entity.UserInTeamInTasks?.Select(u => _userInTeamInTaskBLLMapper.Map(u)).ToList()!
        };
        
        return res;
    }
    
    public static UserInTeam? MapSimple(DTO.UserInTeam? entity)
    {
        if (entity == null) return null;

        return new UserInTeam()
        {
            Id = entity.Id,
            TeamRoleId = entity.TeamRoleId,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            TeamId = entity.TeamId
        };
    }
    
    public static DTO.UserInTeam? MapSimple(UserInTeam? entity)
    {
        if (entity == null) return null;

        return new DTO.UserInTeam()
        {
            Id = entity.Id,
            TeamRoleId = entity.TeamRoleId,
            Since = entity.Since,
            Until = entity.Until,
            UserId = entity.UserId,
            TeamId = entity.TeamId
        };
    }
}