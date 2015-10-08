﻿using Core.Exceptions;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class EditCashgameTests : TestBase
    {
        [Test]
        public void EditCashgame_EmptyLocation_ThrowsException()
        {
            var request = new EditCashgame.Request(TestData.ManagerUser.UserName, TestData.CashgameIdA, 0, 0);

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void EditCashgame_ValidLocation_ReturnUrlIsSet()
        {
            var request = new EditCashgame.Request(TestData.ManagerUser.UserName, TestData.CashgameIdA, TestData.ChangedLocationId, 0);

            var result = Sut.Execute(request);

            Assert.AreEqual(1, result.CashgameId);
        }

        [Test]
        public void EditCashgame_ValidLocation_SavesCashgame()
        {
            var request = new EditCashgame.Request(TestData.ManagerUser.UserName, TestData.CashgameIdA, TestData.ChangedLocationId, 0);

            Sut.Execute(request);

            Assert.AreEqual(TestData.BunchA.Id, Repos.Cashgame.Updated.Id);
            Assert.AreEqual(TestData.ChangedLocationId, Repos.Cashgame.Updated.LocationId);
        }

        private EditCashgame Sut
        {
            get
            {
                return new EditCashgame(
                    Services.CashgameService,
                    Services.UserService,
                    Services.PlayerService,
                    Services.LocationService,
                    Services.EventService);
            }
        }
    }
}