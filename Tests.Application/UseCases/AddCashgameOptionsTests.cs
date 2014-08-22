﻿using System.Collections.Generic;
using Application.Exceptions;
using Application.UseCases.CashgameOptions;
using Core.Entities;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class AddCashgameOptionsTests : MockContainer
    {
        [Test]
        public void AddCashgameOptions_ReturnsResultObject()
        {
            const string slug = "a";
            var result = Sut.Execute(new CashgameOptionsRequest(slug));

            Assert.IsInstanceOf<CashgameOptionsResult>(result);
        }

        [Test]
        public void AddCashgameOptions_WithRunningCashgame_ThrowsException()
        {
            const string slug = "a";

            GetMock<ICashgameRepository>().Setup(o => o.GetRunning(It.IsAny<Homegame>())).Returns(new CashgameInTest());

            Assert.Throws<CashgameRunningException>(() => Sut.Execute(new CashgameOptionsRequest(slug)));
        }

        [Test]
        public void AddCashgameOptions_LocationsAreSet()
        {
            const string location1 = "a";
            const string location2 = "b";
            var locations = new List<string> { location1, location2 };

            GetMock<ICashgameRepository>().Setup(o => o.GetLocations(It.IsAny<Homegame>())).Returns(locations);

            const string slug = "a";
            var result = Sut.Execute(new CashgameOptionsRequest(slug));

            Assert.AreEqual(2, result.Locations.Count);
            Assert.AreEqual(location1, result.Locations[0]);
            Assert.AreEqual(location2, result.Locations[1]);
        }

        private CashgameOptionsInteractor Sut
        {
            get
            {
                return new CashgameOptionsInteractor(
                    GetMock<IHomegameRepository>().Object,
                    GetMock<ICashgameRepository>().Object);
            }
        }
    }
}