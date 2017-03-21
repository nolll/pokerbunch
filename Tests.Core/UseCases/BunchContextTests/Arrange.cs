using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.BunchContextTests
{
    public abstract class Arrange : UseCaseTest<BunchContext>
    {
        protected BunchContext.Result Result;

        protected abstract string BunchId { get; }
        protected virtual bool UserHasBunches => false;

        protected override void Setup()
        {
            var singleBunch = new Bunch(BunchId);
            var smallBunch = new SmallBunch(BunchId);
            var bunchList = UserHasBunches ? new List<SmallBunch> {smallBunch} : new List<SmallBunch>();
            Mock<IBunchRepository>().Setup(o => o.Get(BunchId)).Returns(singleBunch);
            Mock<IBunchRepository>().Setup(o => o.ListForUser()).Returns(bunchList);
        }

        protected override void Execute()
        {
            var baseContext = new BaseContext.Result("1");
            var coreContext = new CoreContext.Result(baseContext, true, false, UserData.UserName1, UserData.DisplayName1);
            Result = Subject.Execute(coreContext, new BunchContext.Request(BunchId));
        }
    }
}