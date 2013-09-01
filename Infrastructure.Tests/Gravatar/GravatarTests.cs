using Infrastructure.Integration.Gravatar;
using NUnit.Framework;
using Tests.Common;

namespace Infrastructure.Tests.Gravatar{

	public class GravatarTests : MockContainer {

	    private const string TestEmail = "henriks@gmail.com";
	    private const string TestHash = "abcdef";

	    [Test]
		public void SmallGravatarUrl(){
	        const string expectedUrlFormat = "http://www.gravatar.com/avatar/{0}?s=40&d=site-url/FrontEnd/Images/pix.gif";
	        var expected = string.Format(expectedUrlFormat, TestHash);
	        EncryptionServiceMock.Setup(o => o.GetMd5Hash(TestEmail)).Returns(TestHash);
            SettingsMock.Setup(o => o.GetSiteUrl()).Returns("site-url");
			
            var sut = GetSut();

			var result = sut.GetSmallAvatarUrl(TestEmail);
			Assert.AreEqual(expected, result);
		}

        [Test]
		public void LargeGravatarUrl(){
			const string expectedUrlFormat = "http://www.gravatar.com/avatar/{0}?s=100&d=site-url/FrontEnd/Images/pix.gif";
            var expected = string.Format(expectedUrlFormat, TestHash);
            EncryptionServiceMock.Setup(o => o.GetMd5Hash(TestEmail)).Returns(TestHash);
			SettingsMock.Setup(o => o.GetSiteUrl()).Returns("site-url");
            
            var sut = GetSut();

            var result = sut.GetLargeAvatarUrl(TestEmail);
			Assert.AreEqual(expected, result);
		}

		private GravatarService GetSut(){
			return new GravatarService(SettingsMock.Object, EncryptionServiceMock.Object);
		}

	}

}