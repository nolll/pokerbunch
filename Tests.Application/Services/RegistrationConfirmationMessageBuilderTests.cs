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

            var result = RegistrationConfirmationMessageBuilder.GetSubject();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetBody_EqualsExpectedSubjectAndContainsCorrectValues_Expected()
        {
            const string expected =
@"Thanks for registering with Poker Bunch.

Here is your password:
a

Please sign in here: http://pokerbunch.com/-/auth/login";
            const string password = "a";

            var result = RegistrationConfirmationMessageBuilder.GetBody(password);

            Assert.AreEqual(expected, result);
        }
    }
}
