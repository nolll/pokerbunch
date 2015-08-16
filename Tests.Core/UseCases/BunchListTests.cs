using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class BunchListTests : TestBase
    {
        [Test]
        public void BunchList_ReturnsListOfBunchItems()
        {
            var result = Sut.Execute(new BunchList.AllBunchesRequest(TestData.AdminUser.UserName));

            Assert.AreEqual(2, result.Bunches.Count);
            Assert.AreEqual("/bunch/details/bunch-a", result.Bunches[0].Url.Relative);
            Assert.AreEqual(TestData.BunchA.DisplayName, result.Bunches[0].DisplayName);
            Assert.AreEqual("/bunch/details/bunch-b", result.Bunches[1].Url.Relative);
            Assert.AreEqual(TestData.BunchB.DisplayName, result.Bunches[1].DisplayName);
        }

        private BunchList Sut
        {
            get { return new BunchList(Repos.Bunch, Repos.User); }
        }
    }
}
