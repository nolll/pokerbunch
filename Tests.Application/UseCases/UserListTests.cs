using System.Collections.Generic;
using System.Linq;
using Application.UseCases.UserList;
using Core.Entities;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    public class UserListTests : MockContainer
    {
        [Test]
        public void UserList_ReturnsListOfUserItems()
        {
            const int expected = 1;
            const string userName = "a";
            var users = new List<User> {new UserInTest(userName: userName)};

            GetMock<IUserRepository>().Setup(o => o.GetList()).Returns(users);

            var result = Sut.Execute();

            Assert.AreEqual(expected, result.Users.Count);
            Assert.AreEqual(userName, result.Users.First().DisplayName);
            Assert.AreEqual(userName, result.Users.First().UserName);
        }

        private UserListInteractor Sut
        {
            get
            {
                return new UserListInteractor(
                    GetMock<IUserRepository>().Object);
            }
        }
    }
}
