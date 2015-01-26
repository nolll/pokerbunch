﻿using System.Linq;
using Core.UseCases.UserList;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class UserListTests : TestBase
    {
        [Test]
        public void UserList_ReturnsListOfUserItems()
        {
            var result = Execute();

            Assert.AreEqual(3, result.Users.Count);
            Assert.AreEqual(Constants.UserDisplayNameA, result.Users.First().DisplayName);
            Assert.AreEqual("/-/user/details/user-name-a", result.Users.First().Url.Relative);
        }

        private UserListResult Execute()
        {
            return UserListInteractor.Execute(Repos.User);
        }
    }
}
