using System.Collections.Generic;
using Application.Services;
using Core.UseCases;
using Core.UseCases.UserList;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.UserModelFactories;

namespace Tests.Web.ModelFactoryTests.UserModelFactories
{
    class UserListItemsModelFactoryTests : MockContainer
    {
        private UserListItemModelFactory _sut;

        [SetUp]
        public virtual void SetUp()
        {
            _sut = new UserListItemModelFactory(
                GetMock<IUrlProvider>().Object);
        }

        [Test]
        public void Create_WithUserListItem_AllPropertiesAreSet()
        {
            const string name = "a";
            const string identifier = "b";
            const string url = "c";
            var userItem = new UserListItem(name, identifier);

            GetMock<IUrlProvider>().Setup(o => o.GetUserDetailsUrl(identifier)).Returns(url);

            var result = _sut.Create(userItem);

            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(url, result.Url);
        }

        [Test]
        public void CreateList_WithUserListItems_ReturnsListOfCorrectLength()
        {
            var userListItems = new List<UserListItem> {new UserListItem()};

            var result = _sut.CreateList(userListItems);

            Assert.AreEqual(1, result.Count);
        }
    }
}
