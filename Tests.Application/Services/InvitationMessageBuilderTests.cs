using Application.Services;
using Application.Services.Interfaces;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.Services
{
    public class InvitationMessageBuilderTests : MockContainer
    {
        [Test]
        public void GetSubject_EqualsExpectedSubject()
        {
            const string displayName = "a";
            const string expected = "Invitation to Poker Bunch: a";
            var homegame = new FakeHomegame(displayName: displayName);

            var sut = GetSut();
            var result = sut.GetSubject(homegame);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetBody_EqualsExpectedSubjectAndContainsCorrectValues_Expected()
        {
            const string displayName = "a";
            const string slug = "b";
            const string siteUrl = "c";
            const string joinUrl = "d";
            const string invitationCode = "e";
            const string addUserUrl = "f";

            const string expected =
@"You have been invited to join the poker game: a.

To accept this invitation, go to cd and enter this verification code: e

If you don't have an account, you can register at cf";
            var homegame = new FakeHomegame(slug: slug, displayName: displayName);
            var player = new FakePlayer();

            GetMock<ISettings>().Setup(o => o.GetSiteUrl()).Returns(siteUrl);
            GetMock<IInvitationCodeCreator>().Setup(o => o.GetCode(player)).Returns(invitationCode);
            GetMock<IUrlProvider>().Setup(o => o.GetAddUserUrl()).Returns(addUserUrl);
            GetMock<IUrlProvider>().Setup(o => o.GetJoinHomegameUrl(slug)).Returns(joinUrl);

            var sut = GetSut();
            var result = sut.GetBody(homegame, player);

            Assert.AreEqual(expected, result);
        }

        private InvitationMessageBuilder GetSut()
        {
            return new InvitationMessageBuilder(
                GetMock<ISettings>().Object,
                GetMock<IUrlProvider>().Object,
                GetMock<IInvitationCodeCreator>().Object);
        }
    }
}
