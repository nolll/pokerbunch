﻿using Application.UseCases.AppContext;

namespace Application.UseCases.BaseContext
{
    public interface IBaseContextInteractor
    {
        BaseContextResult Execute();
    }
}