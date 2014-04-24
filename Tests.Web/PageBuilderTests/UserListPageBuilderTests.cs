using System.Collections.Generic;
using Application.UseCases.UserList;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.PageBaseModelFactories;
using Web.ModelFactories.UserModelFactories;
using Web.Models.PageBaseModels;
using Web.Models.UserModels.List;

namespace Tests.Web.PageBuilderTests
{
    class UserListPageBuilderTests : MockContainer
    {
        private UserListPageBuilder _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UserListPageBuilder(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<IUserListItemModelFactory>().Object,
                GetMock<IUserListInteractor>().Object);
        }

        [Test]
        public void Build_BrowserTitleIsSet()
        {
            var showUserListResult = new UserListResult();

            GetMock<IUserListInteractor>().Setup(o => o.Execute()).Returns(showUserListResult);

            var result = _sut.Build();

            Assert.AreEqual("Users", result.BrowserTitle);
        }

        [Test]
        public void Build_PagePropertiesIsSet()
        {
            var pageProperties = new PageProperties();
            var showUserListResult = new UserListResult();

            GetMock<IUserListInteractor>().Setup(o => o.Execute()).Returns(showUserListResult);
            GetMock<IPagePropertiesFactory>().Setup(o => o.Create((Homegame) null)).Returns(pageProperties);

            var result = _sut.Build();

            Assert.IsNotNull(result.PageProperties);
        }

        [Test]
        public void Build_BunchModelsAreSet()
        {
            var userItems = new List<UserListItem>();
            var showUserListResult = new UserListResult { Users = userItems };
            var models = new List<UserListItemModel>();

            GetMock<IUserListInteractor>().Setup(o => o.Execute()).Returns(showUserListResult);
            GetMock<IUserListItemModelFactory>().Setup(o => o.CreateList(userItems)).Returns(models);

            var result = _sut.Build();

            Assert.AreEqual(userItems, result.UserModels);
        }
    }
}
