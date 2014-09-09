using Application.Services;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.Services
{
    public class PasswordMessageBuilderTests : TestBase
    {
        [Test]
        public void GetSubject_EqualsExpectedSubject()
        {
            const string expected = "Poker Bunch password recovery";

            var result = PasswordMessageBuilder.GetSubject();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetBody_EqualsExpectedSubjectAndContainsCorrectValues_Expected()
        {
            const string expected =
@"Here is your new password for Poker Bunch:
a

Please sign in here: http://pokerbunch.com/-/auth/login";
            const string password = "a";

            var result = PasswordMessageBuilder.GetBody(password);

            Assert.AreEqual(expected, result);
        }
    }
}
