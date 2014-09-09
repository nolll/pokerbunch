using Application.Services;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.Services
{
    public class InvitationMessageBuilderTests : TestBase
    {
        [Test]
        public void GetSubject_EqualsExpectedSubject()
        {
            const string displayName = "a";
            const string expected = "Invitation to Poker Bunch: a";
            var homegame = new BunchInTest(displayName: displayName);

            var result = InvitationMessageBuilder.GetSubject(homegame);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetBody_EqualsExpectedSubjectAndContainsCorrectValues_Expected()
        {
            const string displayName = "a";
            const string slug = "b";

            const string expected =
@"You have been invited to join the poker game: a.

To accept this invitation, go to http://pokerbunch.com/b/homegame/join and enter this verification code: 9d2f82be03d5bae28167fff215bce098b7049984

If you don't have an account, you can register at http://pokerbunch.com/-/user/add";
            var homegame = new BunchInTest(slug: slug, displayName: displayName);
            var player = new PlayerInTest();

            var result = InvitationMessageBuilder.GetBody(homegame, player);

            Assert.AreEqual(expected, result);
        }
    }
}
