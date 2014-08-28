using Application.UseCases.AddBunchForm;
using Application.UseCases.JoinBunchForm;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class JoinBunchFormTests : MockContainer
    {
        private const string Slug = "a";
        private const string BunchName = "b";

        [Test]
        public void JoinBunchForm_BunchNameIsSet()
        {
            var bunch = new BunchInTest(displayName: BunchName);
            var request = new JoinBunchFormRequest(Slug);
            
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(bunch);

            var result = Sut.Execute(request);

            Assert.AreEqual(BunchName, result.BunchName);
        }

        private JoinBunchFormInteractor Sut
        {
            get
            {
                return new JoinBunchFormInteractor(
                    GetMock<IBunchRepository>().Object);
            }
        }
    }
}
