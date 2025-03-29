using Base.Contracts;

namespace App.DAL.Contracts;

public interface IAppUOW : IBaseUOW
{
    IPersonRepository PersonRepository { get; }
}