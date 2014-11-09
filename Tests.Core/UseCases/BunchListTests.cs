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
            Assert.AreEqual("/bunch-a/homegame/details", result.Bunches[0].Url.Relative);
            Assert.AreEqual(Constants.BunchNameA, result.Bunches[0].DisplayName);
            Assert.AreEqual("/bunch-b/homegame/details", result.Bunches[1].Url.Relative);
            Assert.AreEqual(Constants.BunchNameB, result.Bunches[1].DisplayName);
        }

        private BunchListResult Execute()
        {
            return BunchListInteractor.Execute(Repo.Bunch);
        }
    }
}
