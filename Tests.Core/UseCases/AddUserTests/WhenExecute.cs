using Core.Entities;
using NUnit.Framework;

namespace Tests.Core.UseCases.AddUserTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void UserIsAdded()
        {
            Assert.AreEqual("", Added.Id);
            Assert.AreEqual(UserName, Added.UserName);
            Assert.AreEqual(DisplayName, Added.DisplayName);
            Assert.AreEqual("", Added.RealName);
            Assert.AreEqual(Email, Added.Email);
            Assert.AreEqual(Role.Player, Added.GlobalRole);
            Assert.AreEqual(Password, SentPassword);
        }
    }
}
