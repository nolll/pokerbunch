using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.UseCases.UserList;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    public class UserListTests : TestBase
    {
        [Test]
        public void UserList_ReturnsListOfUserItems()
        {
            const int expected = 1;
            const string userName = "a";
            var users = new List<User> {new UserInTest(userName: userName)};

            GetMock<IUserRepository>().Setup(o => o.GetList()).Returns(users);

            var result = Execute();

            Assert.AreEqual(expected, result.Users.Count);
            Assert.AreEqual(userName, result.Users.First().DisplayName);
            Assert.AreEqual(userName, result.Users.First().UserName);
        }

        private UserListResult Execute()
        {
            return UserListInteractor.Execute(GetMock<IUserRepository>().Object);
        }
    }
}
