using Core.Exceptions;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class JoinBunchTests : TestBase
    {
        private const string ValidCode = "d643c7857f8c3bffb1e9e7017a5448d09ef59d33";

        [Test]
        public void JoinBunch_EmptyCode_ThrowsValidationException()
        {
            const string code = "";
            var request = new JoinBunch.Request(TestData.SlugA, TestData.UserNameA, code);

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void JoinBunch_InvalidCode_InvalidJoinCodeException()
        {
            const string code = "abc";
            var request = new JoinBunch.Request(TestData.SlugA, TestData.UserNameA, code);

            Assert.Throws<InvalidJoinCodeException>(() => Sut.Execute(request));
        }

        [Test]
        public void JoinBunch_ValidCode_JoinsBunch()
        {
            var request = new JoinBunch.Request(TestData.SlugA, TestData.UserNameA, ValidCode);

            var result = Sut.Execute(request);
            Assert.AreEqual("/bunch-a/homegame/joined", result.ReturnUrl.Relative);
        }

        [Test]
        public void JoinBunch_ValidCode_ReturnsConfirmationUrl()
        {
            var request = new JoinBunch.Request(TestData.SlugA, TestData.UserNameA, ValidCode);

            Sut.Execute(request);
            Assert.AreEqual(TestData.PlayerA.Id, Repos.Player.Joined.PlayerId);
            Assert.AreEqual(TestData.BunchA.Id, Repos.Player.Joined.BunchId);
            Assert.AreEqual(TestData.UserA.Id, Repos.Player.Joined.UserId);
        }

        private JoinBunch Sut
        {
            get
            {
                return new JoinBunch(
                    Repos.Bunch,
                    Repos.Player,
                    Repos.User);
            }
        }
    }
}