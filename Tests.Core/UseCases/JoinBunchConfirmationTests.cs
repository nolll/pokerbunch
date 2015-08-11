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
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(TestData.BunchA.DisplayName, result.BunchName);
        }

        [Test]
        public void JoinBunchConfirmation_BunchDetailsUrlIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.IsInstanceOf<BunchDetailsUrl>(result.BunchDetailsUrl);
        }

        private static JoinBunchConfirmationInteractor.JoinBunchConfirmationRequest CreateRequest()
        {
            return new JoinBunchConfirmationInteractor.JoinBunchConfirmationRequest(TestData.SlugA);
        }

        private JoinBunchConfirmationInteractor Sut
        {
            get
            {
                return new JoinBunchConfirmationInteractor(
                    Repos.Bunch);
            }
        }
    }
}
