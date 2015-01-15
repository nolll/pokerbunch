using System;
using Core.Entities;
using Core.Urls;
using Core.UseCases.Actions;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class ActionsTests : TestBase
    {
        private const string BuyinDescription = "Buyin";
        private const string CashoutDescription = "Cashout";

        [Test]
        public void Actions_ActionsResultIsReturned()
        {
            var request = new ActionsInput(Constants.SlugA, Constants.DateStringA, Constants.PlayerIdA);
            var result = Execute(request);

            Assert.AreEqual(DateTime.Parse("2001-02-03 04:05:06"), result.Date);
            Assert.AreEqual(Constants.PlayerNameA, result.PlayerName);
            Assert.AreEqual(2, result.CheckpointItems.Count);
        }

        [Test]
        public void Actions_ItemPropertiesAreSet()
        {
            var request = new ActionsInput(Constants.SlugA, Constants.DateStringA, Constants.PlayerIdA);
            var result = Execute(request);

            Assert.AreEqual(BuyinDescription, result.CheckpointItems[0].Type);
            Assert.AreEqual(200, result.CheckpointItems[0].DisplayAmount.Amount);
            Assert.AreEqual(DateTime.Parse("2001-02-03 03:05:06"), result.CheckpointItems[0].Time);
            Assert.IsFalse(result.CheckpointItems[0].CanEdit);
            Assert.IsInstanceOf<EditCheckpointUrl>(result.CheckpointItems[0].EditUrl);

            Assert.AreEqual(CashoutDescription, result.CheckpointItems[1].Type);
            Assert.AreEqual(50, result.CheckpointItems[1].DisplayAmount.Amount);
        }

        [Test]
        public void Actions_WithManager_CanEditIsTrueOnItem()
        {
            var request = new ActionsInput(Constants.SlugA, Constants.DateStringA, Constants.PlayerIdA);

            Services.Auth.SetCurrentRole(Role.Manager);
            
            var result = Execute(request);

            Assert.IsTrue(result.CheckpointItems[0].CanEdit);
        }

        private ActionsOutput Execute(ActionsInput input)
        {
            return ActionsInteractor.Execute(
                Repos.Bunch,
                Repos.Cashgame,
                Repos.Player,
                Services.Auth,
                input);
        }
    }
}