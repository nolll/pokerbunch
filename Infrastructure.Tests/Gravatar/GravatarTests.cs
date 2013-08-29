using Infrastructure.Integration.Gravatar;
using NUnit.Framework;
using Tests.Common;

namespace Infrastructure.Tests.Gravatar{

	public class GravatarTests : MockContainer {

        [Test]
		public void SmallGravatarUrl(){
			const string gravatarEmail = "henriks@gmail.com";
            const string expectedUrl = "http://www.gravatar.com/avatar/24a827c683a7646cde86696b418b20b4?s=40&d=site-url/FrontEnd/Images/pix.gif";
            SettingsMock.Setup(o => o.GetSiteUrl()).Returns("site-url");
			
            var sut = GetSut();

			var result = sut.getSmallAvatarUrl(gravatarEmail);
			Assert.AreEqual(expectedUrl, result);
		}

        [Test]
		public void LargeGravatarUrl(){
			const string gravatarEmail = "henriks@gmail.com";
			const string expectedUrl = "http://www.gravatar.com/avatar/24a827c683a7646cde86696b418b20b4?s=100&d=site-url/FrontEnd/Images/pix.gif";
			SettingsMock.Setup(o => o.GetSiteUrl()).Returns("site-url");
            
            var sut = GetSut();

			var result = sut.getLargeAvatarUrl(gravatarEmail);
			Assert.AreEqual(expectedUrl, result);
		}

		private GravatarService GetSut(){
			return new GravatarService(SettingsMock.Object);
		}

	}

}