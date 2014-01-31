using Application.Services;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.Services
{
    public class RegistrationConfirmationMessageBuilderTests : MockContainer
    {
        [Test]
        public void GetSubject_EqualsExpectedSubject()
        {
            const string expected = "Poker Bunch Registration";

            var sut = GetSut();
            var result = sut.GetSubject();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetBody_EqualsExpectedSubjectAndContainsCorrectValues_Expected()
        {
            const string expected =
@"Thanks for registering with Poker Bunch.

Here is your password:
a

Please sign in here: bc";
            const string password = "a";
            const string siteUrl = "b";
            const string loginUrl = "c";

            GetMock<ISettings>().Setup(o => o.GetSiteUrl()).Returns(siteUrl);
            GetMock<IUrlProvider>().Setup(o => o.GetLoginUrl()).Returns(loginUrl);

            var sut = GetSut();
            var result = sut.GetBody(password);

            Assert.AreEqual(expected, result);
        }

        private RegistrationConfirmationMessageBuilder GetSut()
        {
            return new RegistrationConfirmationMessageBuilder(
                GetMock<ISettings>().Object,
                GetMock<IUrlProvider>().Object);
        }
    }
}
