using Application.Services;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.Services
{
	public class InvitationCodeCreatorTests : TestBase
    {
        [Test]
		public void GetCode_ReturnsEncryptedPlayerName()
        {
            const string playerName = "a";
			var player = new PlayerInTest(displayName: playerName);

            var result = InvitationCodeCreator.GetCode(player);

            Assert.AreEqual("b8260ca84de1d8bc2ed1126d0096dbaadd4db2fe", result);
		}
	}
}