using Application.Urls;
using Application.UseCases.UserList;
using NUnit.Framework;
using Tests.Common;
using Web.Models.UrlModels;
using Web.Models.UserModels.List;

namespace Tests.Web.ModelTests.UserModels
{
    class UserListItemsModelTests : MockContainer
    {
        [Test]
        public void Create_WithUserListItem_AllPropertiesAreSet()
        {
            const string name = "a";
            const string identifier = "b";
            var userItem = new UserListItem(name, identifier);

            var result = new UserListItemModel(userItem);

            Assert.AreEqual(name, result.Name);
            Assert.IsInstanceOf<UserDetailsUrl>(result.Url);
        }
    }
}
