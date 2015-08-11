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
            var request = new JoinBunchFormInteractor.JoinBunchFormRequest(TestData.SlugA);

            var result = Sut.Execute(request);

            Assert.AreEqual(TestData.BunchA.DisplayName, result.BunchName);
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
