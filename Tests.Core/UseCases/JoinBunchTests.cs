using Core.Exceptions;
using Core.UseCases.JoinBunch;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class JoinBunchTests : TestBase
    {
        [Test]
        public void JoinBunch_EmptyCode_ThrowsValidationException()
        {
            const string code = "";
            var request = new JoinBunchRequest(Constants.SlugA, code);

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void JoinBunch_InvalidCode_InvalidJoinCodeException()
        {
            const string code = "abc";
            var request = new JoinBunchRequest(Constants.SlugA, code);

            Assert.Throws<InvalidJoinCodeException>(() => Sut.Execute(request));
        }

        [Test]
        public void JoinBunch_ValidCode_InvalidJoinCodeException()
        {
            const string code = "d643c7857f8c3bffb1e9e7017a5448d09ef59d33";
            var request = new JoinBunchRequest(Constants.SlugA, code);

            var result = Sut.Execute(request);
            Assert.AreEqual("/bunch-a/homegame/joined", result.ReturnUrl.Relative);
        }

        private JoinBunchInteractor Sut
        {
            get
            {
                return new JoinBunchInteractor(
                    Services.Auth,
                    Repos.Bunch,
                    Repos.Player);
            }
        }
    }
}