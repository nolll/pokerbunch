using Core.UseCases.BunchList;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class BunchListTests : TestBase
    {
        [Test]
        public void BunchList_ReturnsListOfBunchItems()
        {
            var result = Execute();

            Assert.AreEqual(2, result.Bunches.Count);
            Assert.AreEqual(Constants.SlugA, result.Bunches[0].Slug);
            Assert.AreEqual(Constants.BunchNameA, result.Bunches[0].DisplayName);
            Assert.AreEqual(Constants.SlugB, result.Bunches[1].Slug);
            Assert.AreEqual(Constants.BunchNameB, result.Bunches[1].DisplayName);
        }

        private BunchListResult Execute()
        {
            return BunchListInteractor.Execute(Repo.Bunch);
        }
    }
}
