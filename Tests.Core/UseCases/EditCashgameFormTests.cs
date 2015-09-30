using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class EditCashgameFormTests : TestBase
    {
        [Test]
        public void EditCashgameForm_AllPropertiesAreSet()
        {
            var result = Sut.Execute(new EditCashgameForm.Request(TestData.ManagerUser.UserName, TestData.CashgameIdA));

            Assert.AreEqual(1, result.CashgameId);
            Assert.AreEqual(TestData.LocationNameA, result.Location);
            Assert.AreEqual(TestData.DateStringA, result.Date);
        }
        
        [Test]
        public void EditCashgameForm_LocationsAreSet()
        {
            var result = Sut.Execute(new EditCashgameForm.Request(TestData.ManagerUser.UserName, TestData.CashgameIdA));

            Assert.AreEqual(2, result.Locations.Count);
            Assert.AreEqual(TestData.LocationNameA, result.Locations[0]);
            Assert.AreEqual(TestData.LocationNameB, result.Locations[1]);
        }

        private EditCashgameForm Sut
        {
            get
            {
                return new EditCashgameForm(
                    Services.BunchService,
                    Services.CashgameService,
                    Services.UserService,
                    Services.PlayerService,
                    Services.LocationService);
            }
        }        
    }
}