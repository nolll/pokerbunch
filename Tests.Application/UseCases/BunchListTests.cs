using Application.UseCases.BunchList;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.UseCases
{
    public class BunchListTests : MockContainer
    {
        [Test]
        public void BunchList_ReturnsListOfBunchItems()
        {
            var homegames = ABunchList.WithOneItem().Build();

            GetMock<IBunchRepository>().Setup(o => o.GetList()).Returns(homegames);

            var result = Sut.Execute();

            Assert.AreEqual(1, result.Bunches.Count);
            Assert.AreEqual("a", result.Bunches[0].Slug);
            Assert.AreEqual("b", result.Bunches[0].DisplayName);
        }

        private BunchListInteractor Sut
        {
            get
            {
                return new BunchListInteractor(
                    GetMock<IBunchRepository>().Object);
            }
        }
    }
}
