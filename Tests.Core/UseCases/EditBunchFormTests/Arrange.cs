using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EditBunchFormTests
{
    public class Arrange : UseCaseTest<EditBunchForm>
    {
        protected EditBunchForm.Result Result;

        protected const string BunchId = BunchData.Id1;
        private const string BunchName = "bunch-name";
        protected const string Description = "description";
        protected const string HouseRules = "house-rules";
        protected const int DefaultBuyin = 200;

        protected override void Setup()
        {
            var bunch = new Bunch(BunchId, BunchName, Description, HouseRules, null, DefaultBuyin);

            Mock<IBunchRepository>().Setup(o => o.Get(BunchId)).Returns(bunch);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new EditBunchForm.Request(BunchId));
        }
    }
}