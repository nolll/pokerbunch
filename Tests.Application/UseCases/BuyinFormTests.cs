using Application.UseCases.BuyinForm;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class BuyinFormTests : MockContainer
    {
        private const string Slug = "a";

        [TestCase(1)]
        [TestCase(2)]
        public void BuyinForm_BuyinAmountIsSetFromHomegameDefaultAmount(int defaultBuyin)
        {
            var homegame = new HomegameInTest(defaultBuyin: defaultBuyin);
            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(Slug)).Returns(homegame);

            var request = new BuyinFormRequest {Slug = Slug};
            var result = Sut.Execute(request);

            Assert.AreEqual(defaultBuyin, result.BuyinAmount);
        }

        private BuyinFormInteractor Sut
        {
            get
            {
                return new BuyinFormInteractor(
                    GetMock<IHomegameRepository>().Object);
            }
        }
    }
}
