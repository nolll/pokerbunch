﻿using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserListPageModelFactory : IUserListPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUserItemModelFactory _userItemModelFactory;

        public UserListPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IUserItemModelFactory userItemModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _userItemModelFactory = userItemModelFactory;
        }

        public UserListPageModel Create(User user, IList<User> users)
        {
            return new UserListPageModel(
                    "Users",
                    _pagePropertiesFactory.Create(user),
                    GetUserModels(users)
                );
        }
        
        private List<UserItemModel> GetUserModels(IEnumerable<User> users)
        {
            return users.Select(_userItemModelFactory.Create).ToList();
        }
    }
}
