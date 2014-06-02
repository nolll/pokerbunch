﻿using Application.UseCases.CashgameContext;

namespace Application.UseCases.ApplicationContext
{
    public interface IApplicationContextInteractor
    {
        ApplicationContextResult Execute();
    }
}