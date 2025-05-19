using App.BLL.Contracts.Services;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;

namespace App.BLL.Services;

public class AbsenceService : BaseService<App.BLL.DTO.Absence, App.DAL.DTO.Absence, App.DAL.Contracts.Repositories.IAbsenceRepository>, IAbsenceService
{
    public AbsenceService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.Absence, Absence> bllMapper) : base(serviceUOW, serviceUOW.AbsenceRepository, bllMapper)
    {
    }

    public async Task<IEnumerable<App.BLL.DTO.Absence?>> GetAbsencesSentToUserInTeam(Guid userId, Guid teamId)
    {
        var res = await ServiceRepository.GetAbsencesSentToUserInTeam(userId, teamId);
        return res.Select(u => BLLMapper.Map(u)!);
    }

    public async Task<IEnumerable<DTO.Absence>> GetAbsencesSentByPersonId(Guid personId)
    {
        var res = await ServiceRepository.GetAbsencesSentByPersonId(personId);
        return res.Select(u => BLLMapper.Map(u)!);
    }

    public async Task<IEnumerable<DTO.Absence>> GetAbsencesSentToPersonId(Guid personId)
    {
        var res = await ServiceRepository.GetAbsencesSentToPersonId(personId);
        return res.Select(u => BLLMapper.Map(u)!);
    }
}