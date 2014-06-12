using Application.Services;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.Services{

	public class InvitationCodeCreatorTests : MockContainer {

        [Test]
		public void GetCode_ReturnsEncryptedPlayerName()
        {
            const string playerName = "a";
			var player = new PlayerInTest(displayName: playerName);
            GetMock<IEncryptionService>().Setup(o => o.Encrypt(playerName, It.IsAny<string>())).Returns("b");

            var sut = GetSut();
            var result = sut.GetCode(player);

			Assert.AreEqual("b", result);
		}

        private InvitationCodeCreator GetSut()
        {
            return new InvitationCodeCreator(GetMock<IEncryptionService>().Object);
        }

	}

}