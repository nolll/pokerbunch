using Application.Services;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Infrastructure.Gravatar
{
	public class GravatarTests : MockContainer
    {
	    private const string TestEmail = "henriks@gmail.com";

	    [Test]
		public void SmallGravatarUrl()
        {
            const string expected = "http://www.gravatar.com/avatar/24a827c683a7646cde86696b418b20b4?s=40";
            GetMock<ISettings>().Setup(o => o.GetSiteUrl()).Returns("site-url");
			
            var sut = GetSut();

			var result = sut.GetSmallAvatarUrl(TestEmail);
			Assert.AreEqual(expected, result);
		}

        [Test]
		public void LargeGravatarUrl()
        {
            const string expected = "http://www.gravatar.com/avatar/24a827c683a7646cde86696b418b20b4?s=100";
            GetMock<ISettings>().Setup(o => o.GetSiteUrl()).Returns("site-url");
            
            var sut = GetSut();

            var result = sut.GetLargeAvatarUrl(TestEmail);
			Assert.AreEqual(expected, result);
		}

		private GravatarService GetSut()
        {
            return new GravatarService();
		}

	}

}