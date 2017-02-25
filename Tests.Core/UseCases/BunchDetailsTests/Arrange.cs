using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.BunchDetailsTests
{
    public abstract class Arrange : UseCaseTest<BunchDetails>
    {
        protected BunchDetails.Result Result;

        protected abstract Role Role { get; }
    
        [SetUp]
        public void Setup()
        {
            var bunch = BunchData.Bunch1(Role);

            Mock<IBunchRepository>().Setup(s => s.Get(BunchData.Id1)).Returns(bunch);

            Result = Sut.Execute(new BunchDetails.Request(BunchData.Id1));
        }
    }
}
