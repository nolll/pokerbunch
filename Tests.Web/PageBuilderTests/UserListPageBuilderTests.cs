using System.Collections.Generic;
using Application.UseCases.AppContext;
using Application.UseCases.UserList;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.UserModelFactories;

namespace Tests.Web.PageBuilderTests
{
    class UserListPageBuilderTests : MockContainer
    {
        [Test]
        public void Build_BrowserTitleIsSet()
        {
            var showUserListResult = new UserListResultInTest();

            GetMock<IAppContextInteractor>().Setup(o => o.Execute()).Returns(new AppContextResultInTest());
            GetMock<IUserListInteractor>().Setup(o => o.Execute()).Returns(showUserListResult);

            var result = Sut.Build();

            Assert.AreEqual("Users", result.BrowserTitle);
        }

        [Test]
        public void Build_BunchModelsAreSet()
        {
            var userItems = new List<UserListItem>();
            var showUserListResult = new UserListResultInTest(userItems);

            GetMock<IAppContextInteractor>().Setup(o => o.Execute()).Returns(new AppContextResultInTest());
            GetMock<IUserListInteractor>().Setup(o => o.Execute()).Returns(showUserListResult);

            var result = Sut.Build();

            Assert.AreEqual(userItems, result.UserModels);
        }

        private UserListPageBuilder Sut
        {
            get
            {
                return new UserListPageBuilder(
                    GetMock<IAppContextInteractor>().Object,
                    GetMock<IUserListInteractor>().Object);
            }
        }
    }
}
