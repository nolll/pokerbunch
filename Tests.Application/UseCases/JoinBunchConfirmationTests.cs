using Core.Repositories;
using Core.Urls;
using Core.UseCases.JoinBunchConfirmation;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class JoinBunchConfirmationTests : TestBase
    {
        private const string Slug = "a";
        private const string BunchName = "b";

        [Test]
        public void JoinBunchConfirmation_BunchNameIsSet()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.AreEqual(BunchName, result.BunchName);
        }

        [Test]
        public void JoinBunchConfirmation_BunchDetailsUrlIsSet()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<BunchDetailsUrl>(result.BunchDetailsUrl);
        }

        private static JoinBunchConfirmationRequest CreateRequest()
        {
            return new JoinBunchConfirmationRequest(Slug);
        }

        private void SetupBunch()
        {
            var bunch = new BunchInTest(displayName: BunchName);
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(bunch);
        }

        private JoinBunchConfirmationResult Execute(JoinBunchConfirmationRequest request)
        {
            return JoinBunchConfirmationInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                request);
        }
    }
}
