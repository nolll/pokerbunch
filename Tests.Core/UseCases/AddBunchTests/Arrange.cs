using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AddBunchTests
{
    public class Arrange : UseCaseTest<AddBunch>
    {
        protected Bunch Added;

        protected const string DisplayName = "Bunch Display Name";
        protected const string Id = "bunch-display-name";
        protected const string Description = BunchData.Description1;
        protected readonly string CurrencySymbol = CurrencyData.Sek.Symbol;
        protected readonly string CurrencyLayout = CurrencyData.Sek.Layout;
        private readonly string _timezone = TimezoneData.Swedish.Id;

        protected override void Setup()
        {
            Added = null;

            Mock<IBunchRepository>().Setup(o => o.Add(It.IsAny<Bunch>()))
                .Callback((Bunch bunch) => Added = bunch);
        }

        protected override void Execute()
        {
            Subject.Execute(new AddBunch.Request(DisplayName, Description, CurrencySymbol, CurrencyLayout, _timezone));
        }
    }
}