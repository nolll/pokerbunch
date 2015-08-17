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
            var result = Sut.Execute(new EditCashgameForm.Request(TestData.ManagerUser.UserName, TestData.SlugA, TestData.DateStringA));

            Assert.AreEqual("/cashgame/details/1", result.CancelUrl.Relative);
            Assert.AreEqual("/cashgame/delete/1", result.DeleteUrl.Relative);
            Assert.AreEqual(TestData.LocationA, result.Location);
            Assert.AreEqual(TestData.DateStringA, result.Date);
        }
        
        [Test]
        public void EditCashgameForm_LocationsAreSet()
        {
            var result = Sut.Execute(new EditCashgameForm.Request(TestData.ManagerUser.UserName, TestData.SlugA, TestData.DateStringA));

            Assert.AreEqual(2, result.Locations.Count);
            Assert.AreEqual(TestData.LocationA, result.Locations[0]);
            Assert.AreEqual(TestData.LocationB, result.Locations[1]);
        }

        private EditCashgameForm Sut
        {
            get
            {
                return new EditCashgameForm(
                    Repos.Bunch,
                    Repos.Cashgame,
                    Repos.User,
                    Repos.Player);
            }
        }        
    }
}