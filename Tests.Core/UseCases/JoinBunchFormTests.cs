using Core.Repositories;
using Core.UseCases.JoinBunchForm;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class JoinBunchFormTests : TestBase
    {
        private const string Slug = "a";
        private const string BunchName = "b";

        [Test]
        public void JoinBunchForm_BunchNameIsSet()
        {
            var bunch = A.Bunch.WithDisplayName(BunchName).Build();
            var request = new JoinBunchFormRequest(Slug);
            
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(bunch);

            var result = Execute(request);

            Assert.AreEqual(BunchName, result.BunchName);
        }

        private JoinBunchFormResult Execute(JoinBunchFormRequest request)
        {
            return JoinBunchFormInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                request);
        }
    }
}
