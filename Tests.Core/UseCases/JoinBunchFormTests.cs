using Core.UseCases.JoinBunchForm;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class JoinBunchFormTests : TestBase
    {
        [Test]
        public void JoinBunchForm_BunchNameIsSet()
        {
            var request = new JoinBunchFormRequest(TestData.SlugA);

            var result = Sut.Execute(request);

            Assert.AreEqual(TestData.BunchNameA, result.BunchName);
        }

        private JoinBunchFormInteractor Sut
        {
            get
            {
                return new JoinBunchFormInteractor(
                    Repos.Bunch);
            }
        }
    }
}
