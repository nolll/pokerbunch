﻿using Core.Classes;
using Web.Models.SharingModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public interface ISharingTwitterPageModelFactory
    {
        SharingTwitterPageModel Create(bool isSharing, TwitterCredentials credentials);
    }
}