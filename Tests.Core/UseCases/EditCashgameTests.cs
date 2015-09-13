using Core.Exceptions;
using Core.UseCases;
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
            var request = new EditCashgame.Request(TestData.ManagerUser.UserName, TestData.CashgameIdA, "");

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void EditCashgame_ValidLocation_ReturnUrlIsSet()
        {
            var request = new EditCashgame.Request(TestData.ManagerUser.UserName, TestData.CashgameIdA, ChangedLocation);

            var result = Sut.Execute(request);

            Assert.AreEqual(1, result.CashgameId);
        }

        [Test]
        public void EditCashgame_ValidLocation_SavesCashgame()
        {
            var request = new EditCashgame.Request(TestData.ManagerUser.UserName, TestData.CashgameIdA, ChangedLocation);

            Sut.Execute(request);

            Assert.AreEqual(TestData.BunchA.Id, Repos.Cashgame.Updated.Id);
            Assert.AreEqual(ChangedLocation, Repos.Cashgame.Updated.Location);
        }

        private EditCashgame Sut
        {
            get
            {
                return new EditCashgame(
                    Services.CashgameService,
                    Services.UserService,
                    Services.PlayerService);
            }
        }
    }
}