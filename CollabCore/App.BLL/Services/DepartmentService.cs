using App.BLL.Contracts.Services;
using App.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;
using Department = App.DAL.DTO.Department;

namespace App.BLL.Services;

public class DepartmentService : BaseService<App.BLL.DTO.Department, App.DAL.DTO.Department, App.DAL.Contracts.Repositories.IDepartmentRepository>, IDepartmentService
{
    public DepartmentService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.Department, Department> bllMapper) : base(serviceUOW, serviceUOW.DepartmentRepository, bllMapper)
    {
    }
}