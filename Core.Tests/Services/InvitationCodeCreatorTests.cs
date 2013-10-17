using Core.Classes;
using Core.Services;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Core.Tests.Services{

	public class InvitationCodeCreatorTests : WebMockContainer {

        [Test]
		public void GetCode_ReturnsEncryptedPlayerName()
        {
            const string playerName = "a";
			var player = new Player{DisplayName = playerName};
            Mocks.EncryptionServiceMock.Setup(o => o.Encrypt(playerName, It.IsAny<string>())).Returns("b");

            var sut = GetSut();
            var result = sut.GetCode(player);

			Assert.AreEqual("b", result);
		}

        private InvitationCodeCreator GetSut()
        {
            return new InvitationCodeCreator(Mocks.EncryptionServiceMock.Object);
        }

	}

}