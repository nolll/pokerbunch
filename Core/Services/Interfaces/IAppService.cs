﻿using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface IAppService
    {
        App GetById(string id);
        IList<App> ListAll();
        IList<App> List();
        string Add(string appName);
        string Add(App app);
        void Update(App app);
    }
}