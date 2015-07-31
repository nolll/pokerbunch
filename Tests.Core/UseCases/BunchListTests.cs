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
            var result = Sut.Execute();

            Assert.AreEqual(2, result.Bunches.Count);
            Assert.AreEqual("/bunch-a/homegame/details", result.Bunches[0].Url.Relative);
            Assert.AreEqual(TestData.BunchNameA, result.Bunches[0].DisplayName);
            Assert.AreEqual("/bunch-b/homegame/details", result.Bunches[1].Url.Relative);
            Assert.AreEqual(TestData.BunchNameB, result.Bunches[1].DisplayName);
        }

        private BunchListInteractor Sut
        {
            get { return new BunchListInteractor(Repos.Bunch, Repos.User); }
        }
    }
}
