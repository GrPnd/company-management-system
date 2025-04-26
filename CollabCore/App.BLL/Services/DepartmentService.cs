using App.BLL.Contracts.Services;
using App.BLL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;
using Department = App.DAL.DTO.Department;

namespace App.BLL.Services;

public class DepartmentService : BaseService<App.BLL.DTO.Department, App.DAL.DTO.Department>, IDepartmentService
{
    public DepartmentService(
        IBaseUOW serviceUOW, 
        IBaseRepository<Department> serviceRepository, 
        IBLLMapper<DTO.Department, Department> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}