using Core.Repositories;
using Core.UseCases.BunchList;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.UseCases
{
    public class BunchListTests : TestBase
    {
        [Test]
        public void BunchList_ReturnsListOfBunchItems()
        {
            var homegames = A.BunchList.WithOneItem().Build();

            GetMock<IBunchRepository>().Setup(o => o.GetList()).Returns(homegames);

            var result = Execute();

            Assert.AreEqual(1, result.Bunches.Count);
            Assert.AreEqual("a", result.Bunches[0].Slug);
            Assert.AreEqual("b", result.Bunches[0].DisplayName);
        }

        private BunchListResult Execute()
        {
            return BunchListInteractor.Execute(GetMock<IBunchRepository>().Object);
        }
    }
}
