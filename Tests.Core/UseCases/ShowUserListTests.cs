using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Core.Repositories;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Core.UseCases
{
    public class ShowUserListTests : MockContainer
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
            Assert.AreEqual(userName, result.Users.First().Name);
            Assert.AreEqual(userName, result.Users.First().Identifier);
        }

        private ShowUserList GetSut()
        {
            return new ShowUserList(
                GetMock<IUserRepository>().Object);
        }
    }
}
