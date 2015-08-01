using Core.Exceptions;
using Core.UseCases.EditCashgame;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class EditCashgameTests : TestBase
    {
        private const string ChangedLocation = "ChangedLocation";

        [Test]
        public void EditCashgame_EmptyLocation_ThrowsException()
        {
            var request = new EditCashgameRequest(TestData.SlugA, TestData.DateStringA, "");

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void EditCashgame_ValidLocation_ReturnUrlIsSet()
        {
            var request = new EditCashgameRequest(TestData.SlugA, TestData.DateStringA, ChangedLocation);

            var result = Sut.Execute(request);

            Assert.AreEqual("/bunch-a/cashgame/details/2001-01-01", result.ReturnUrl.Relative);
        }

        [Test]
        public void EditCashgame_ValidLocation_SavesCashgame()
        {
            var request = new EditCashgameRequest(TestData.SlugA, TestData.DateStringA, ChangedLocation);

            Sut.Execute(request);

            Assert.AreEqual(TestData.BunchA.Id, Repos.Cashgame.Updated.Id);
            Assert.AreEqual(ChangedLocation, Repos.Cashgame.Updated.Location);
        }

        private EditCashgameInteractor Sut
        {
            get
            {
                return new EditCashgameInteractor(
                    Repos.Bunch,
                    Repos.Cashgame);
            }
        }
    }
}