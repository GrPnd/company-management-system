﻿using App.DAL.Contracts.Repositories;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IAppUOW : IBaseUOW
{
    IPersonRepository PersonRepository { get; }
}