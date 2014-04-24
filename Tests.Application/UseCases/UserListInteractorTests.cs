using System.Collections.Generic;
using System.Linq;
using Application.UseCases.UserList;
using Core.Classes;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    public class UserListInteractorTests : MockContainer
    {
        [Test]
        public void Execute_WithUsers_ReturnsListOfUserItems()
        {
            const int expected = 1;
            const string userName = "a";
            var users = new List<User> {new FakeUser(userName: userName)};

            GetMock<IUserRepository>().Setup(o => o.GetList()).Returns(users);

            var sut = GetSut();
            var result = sut.Execute();

            Assert.AreEqual(expected, result.Users.Count);
            Assert.AreEqual(userName, result.Users.First().DisplayName);
            Assert.AreEqual(userName, result.Users.First().UserName);
        }

        private UserListInteractor GetSut()
        {
            return new UserListInteractor(
                GetMock<IUserRepository>().Object);
        }
    }
}
