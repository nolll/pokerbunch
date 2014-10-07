using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
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
            const int expected = 1;
            const string userName = "a";
            var user = A.User.WithUserName(userName).Build();
            var users = new List<User> {user};

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
