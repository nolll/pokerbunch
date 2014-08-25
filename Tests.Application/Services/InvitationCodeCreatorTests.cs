using Application.Services;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.Services
{
	public class InvitationCodeCreatorTests : MockContainer
    {
        [Test]
		public void GetCode_ReturnsEncryptedPlayerName()
        {
            const string playerName = "a";
			var player = new PlayerInTest(displayName: playerName);

            var sut = GetSut();
            var result = sut.GetCode(player);

            Assert.AreEqual("b8260ca84de1d8bc2ed1126d0096dbaadd4db2fe", result);
		}

        private InvitationCodeCreator GetSut()
        {
            return new InvitationCodeCreator();
        }
	}
}