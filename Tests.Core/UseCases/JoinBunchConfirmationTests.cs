using Core.Urls;
using Core.UseCases.JoinBunchConfirmation;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class JoinBunchConfirmationTests : TestBase
    {
        [Test]
        public void JoinBunchConfirmation_BunchNameIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(Constants.BunchNameA, result.BunchName);
        }

        [Test]
        public void JoinBunchConfirmation_BunchDetailsUrlIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<BunchDetailsUrl>(result.BunchDetailsUrl);
        }

        private static JoinBunchConfirmationRequest CreateRequest()
        {
            return new JoinBunchConfirmationRequest(Constants.SlugA);
        }

        private JoinBunchConfirmationResult Execute(JoinBunchConfirmationRequest request)
        {
            return JoinBunchConfirmationInteractor.Execute(
                Repos.Bunch,
                request);
        }
    }
}
