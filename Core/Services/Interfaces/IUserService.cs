﻿using Core.Entities;

namespace Core.Services
{
    public interface IUserService
    {
        User Current(string token);
        User GetByNameOrEmail(string nameOrEmail);
    }
}