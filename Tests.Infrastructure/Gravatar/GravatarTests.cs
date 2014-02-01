using Application.Services;
using Infrastructure.Integration.Gravatar;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Infrastructure.Gravatar
{
	public class GravatarTests : MockContainer
    {
	    private const string TestEmail = "henriks@gmail.com";
	    private const string TestHash = "abcdef";

	    [Test]
		public void SmallGravatarUrl()
        {
	        const string expectedUrlFormat = "http://www.gravatar.com/avatar/{0}?s=40";
	        var expected = string.Format(expectedUrlFormat, TestHash);
            GetMock<IEncryptionService>().Setup(o => o.GetMd5Hash(TestEmail)).Returns(TestHash);
            GetMock<ISettings>().Setup(o => o.GetSiteUrl()).Returns("site-url");
			
            var sut = GetSut();

			var result = sut.GetSmallAvatarUrl(TestEmail);
			Assert.AreEqual(expected, result);
		}

        [Test]
		public void LargeGravatarUrl()
        {
			const string expectedUrlFormat = "http://www.gravatar.com/avatar/{0}?s=100";
            var expected = string.Format(expectedUrlFormat, TestHash);
            GetMock<IEncryptionService>().Setup(o => o.GetMd5Hash(TestEmail)).Returns(TestHash);
            GetMock<ISettings>().Setup(o => o.GetSiteUrl()).Returns("site-url");
            
            var sut = GetSut();

            var result = sut.GetLargeAvatarUrl(TestEmail);
			Assert.AreEqual(expected, result);
		}

		private GravatarService GetSut()
        {
            return new GravatarService(
                GetMock<IEncryptionService>().Object);
		}

	}

}