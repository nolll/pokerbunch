using System.Collections.Generic;
using Core.Entities;
using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.BunchListTests
{
    public abstract class Arrange : UseCaseTest<BunchList>
    {
        protected BunchList.Result Result;

        protected override void Setup()
        {
            Mock<IBunchService>().Setup(o => o.List()).Returns(Bunches);
        }

        protected override void Execute()
        {
            Result = Subject.Execute();
        }

        private IList<SmallBunch> Bunches => new List<SmallBunch>
        {
            new SmallBunch(BunchData.Id1, BunchData.DisplayName1),
            new SmallBunch(BunchData.Id2, BunchData.DisplayName2)
        }; 
    }
}