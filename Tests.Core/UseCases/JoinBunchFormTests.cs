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
            var request = new JoinBunchFormRequest(Constants.SlugA);
            
            var result = Execute(request);

            Assert.AreEqual(Constants.BunchNameA, result.BunchName);
        }

        private JoinBunchFormResult Execute(JoinBunchFormRequest request)
        {
            return JoinBunchFormInteractor.Execute(
                Repos.Bunch,
                request);
        }
    }
}
