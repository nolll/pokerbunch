using Core.Exceptions;
using Core.UseCases.JoinBunch;
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
            var request = new JoinBunchRequest(Constants.SlugA, Constants.UserIdA, code);

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void JoinBunch_InvalidCode_InvalidJoinCodeException()
        {
            const string code = "abc";
            var request = new JoinBunchRequest(Constants.SlugA, Constants.UserIdA, code);

            Assert.Throws<InvalidJoinCodeException>(() => Sut.Execute(request));
        }

        [Test]
        public void JoinBunch_ValidCode_JoinsBunch()
        {
            var request = new JoinBunchRequest(Constants.SlugA, Constants.UserIdA, ValidCode);

            var result = Sut.Execute(request);
            Assert.AreEqual("/bunch-a/homegame/joined", result.ReturnUrl.Relative);
        }

        [Test]
        public void JoinBunch_ValidCode_ReturnsConfirmationUrl()
        {
            var request = new JoinBunchRequest(Constants.SlugA, Constants.UserIdA, ValidCode);

            Sut.Execute(request);
            Assert.AreEqual(Constants.PlayerIdA, Repos.Player.Joined.PlayerId);
            Assert.AreEqual(Constants.BunchIdA, Repos.Player.Joined.BunchId);
            Assert.AreEqual(Constants.UserIdA, Repos.Player.Joined.UserId);
        }

        private JoinBunchInteractor Sut
        {
            get
            {
                return new JoinBunchInteractor(
                    Repos.Bunch,
                    Repos.Player);
            }
        }
    }
}