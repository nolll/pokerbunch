using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Classes;
using Infrastructure.System;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Add;
using Web.Models.UserModels.Add;

namespace Web.ModelFactories.UserModelFactories
{
    public class AddUserPageModelFactory : IAddUserPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public AddUserPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public AddUserPageModel Create(User user)
        {
            return new AddUserPageModel
                {
                    BrowserTitle = "Register",
                    PageProperties = _pagePropertiesFactory.Create(user)
                };
        }

        public AddUserPageModel Create(User user, AddUserPostModel postModel)
        {
            var model = Create(user);
            model.UserName = postModel.UserName;
            model.DisplayName = postModel.DisplayName;
            model.Email = postModel.Email;
            return model;
        }

    }
}