using Core.UseCases.EditCashgameForm;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class EditCashgameFormTests : TestBase
    {
        [Test]
        public void EditCashgameForm_AllPropertiesAreSet()
        {
            var result = Sut.Execute(new EditCashgameFormRequest(Constants.SlugA, Constants.DateStringA));

            Assert.AreEqual("/bunch-a/cashgame/details/2001-01-01", result.CancelUrl.Relative);
            Assert.AreEqual("/bunch-a/cashgame/delete/2001-01-01", result.DeleteUrl.Relative);
            Assert.AreEqual(Constants.LocationA, result.Location);
            Assert.AreEqual(Constants.DateStringA, result.Date);
        }
        
        [Test]
        public void EditCashgameForm_LocationsAreSet()
        {
            var result = Sut.Execute(new EditCashgameFormRequest(Constants.SlugA, Constants.DateStringA));

            Assert.AreEqual(2, result.Locations.Count);
            Assert.AreEqual(Constants.LocationA, result.Locations[0]);
            Assert.AreEqual(Constants.LocationB, result.Locations[1]);
        }

        private EditCashgameFormInteractor Sut
        {
            get
            {
                return new EditCashgameFormInteractor(
                    Repos.Bunch,
                    Repos.Cashgame);
            }
        }        
    }
}