using App.DTO.v1.ApiEntities;

namespace App.DTO.v1.ApiMappers;

public class AbsenceApiMapper : IApiMapper<ApiEntities.Absence, App.BLL.DTO.Absence>
{
    public Absence? Map(BLL.DTO.Absence? entity)
    {
        throw new NotImplementedException();
    }

    public BLL.DTO.Absence? Map(Absence? entity)
    {
        throw new NotImplementedException();
    }
}