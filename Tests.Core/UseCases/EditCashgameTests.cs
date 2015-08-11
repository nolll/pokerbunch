﻿using Core.Exceptions;
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
            var request = new EditCashgame.EditCashgameRequest(TestData.SlugA, TestData.DateStringA, "");

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void EditCashgame_ValidLocation_ReturnUrlIsSet()
        {
            var request = new EditCashgame.EditCashgameRequest(TestData.SlugA, TestData.DateStringA, ChangedLocation);

            var result = Sut.Execute(request);

            Assert.AreEqual("/bunch-a/cashgame/details/2001-01-01", result.ReturnUrl.Relative);
        }

        [Test]
        public void EditCashgame_ValidLocation_SavesCashgame()
        {
            var request = new EditCashgame.EditCashgameRequest(TestData.SlugA, TestData.DateStringA, ChangedLocation);

            Sut.Execute(request);

            Assert.AreEqual(TestData.BunchA.Id, Repos.Cashgame.Updated.Id);
            Assert.AreEqual(ChangedLocation, Repos.Cashgame.Updated.Location);
        }

        private EditCashgame Sut
        {
            get
            {
                return new EditCashgame(
                    Repos.Bunch,
                    Repos.Cashgame);
            }
        }
    }
}