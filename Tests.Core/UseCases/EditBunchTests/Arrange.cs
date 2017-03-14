using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EditBunchTests
{
    public abstract class Arrange : UseCaseTest<EditBunch>
    {
        protected EditBunch.Result Result;
        protected Bunch Saved;

        protected const string BunchId = BunchData.Id1;
        private const string DisplayName = BunchData.DisplayName1;
        protected const string Description = "description";
        protected readonly string CurrencySymbol = CurrencyData.Sek.Symbol;
        protected readonly string CurrencyLayout = CurrencyData.Sek.Layout;
        protected readonly string Timezone = TimezoneData.Swedish.Id;
        protected const string HouseRules = "house-rules";
        protected const int DefaultBuyin = 200;

        protected override void Setup()
        {
            Saved = null;
        }

        protected override void Execute()
        {
            var bunch = new Bunch(BunchId, DisplayName);

            Mock<IBunchRepository>().Setup(o => o.Get(BunchId)).Returns(bunch);

            Mock<IBunchRepository>().Setup(o => o.Update(It.IsAny<Bunch>()))
                .Callback((Bunch b) => Saved = b);

            Result = Subject.Execute(new EditBunch.Request(BunchId, Description, CurrencySymbol, CurrencyLayout, Timezone, HouseRules, DefaultBuyin));
        }
    }
}