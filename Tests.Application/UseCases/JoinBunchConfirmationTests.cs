using Application.Urls;
using Application.UseCases.JoinBunchConfirmation;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class JoinBunchConfirmationTests : MockContainer
    {
        private const string Slug = "a";
        private const string BunchName = "b";

        [Test]
        public void JoinBunchConfirmation_BunchNameIsSet()
        {
            SetupBunch();

            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(BunchName, result.BunchName);
        }

        [Test]
        public void JoinBunchConfirmation_BunchDetailsUrlIsSet()
        {
            SetupBunch();

            var result = Sut.Execute(CreateRequest());

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

        private JoinBunchConfirmationInteractor Sut
        {
            get
            {
                return new JoinBunchConfirmationInteractor(
                    GetMock<IBunchRepository>().Object);
            }
        }
    }
}
