using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Application.Services;
using Core.Entities;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UrlModels;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Edit
{
	public class CashgameEditPageModelFactoryTests : MockContainer
    {
		private Homegame _homegame;
		private List<string> _locations;

        [SetUp]
		public void SetUp(){
			_homegame = new HomegameInTest();
			_locations = new List<string>();
		}

        [Test]
		public void IsoDate_IsSet()
        {
            const string formattedStartDate = "a";
            var startTime = DateTime.Parse("2010-01-01 01:00:00");
            var cashgame = new CashgameInTest(startTime: startTime);

            GetMock<IGlobalization>().Setup(o => o.FormatIsoDate(startTime)).Returns(formattedStartDate);

            var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _locations, null);

			Assert.AreEqual(formattedStartDate, result.IsoDate);
		}

		[Test]
		public void CancelUrl_IsSet()
		{
            var cashgame = new CashgameInTest();

            var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _locations, null);

			Assert.IsInstanceOf<CashgameDetailsUrl>(result.CancelUrl);
		}

		[Test]
		public void DeleteUrl_IsSet()
        {
            var cashgame = new CashgameInTest();

            var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _locations, null);

			Assert.IsInstanceOf<DeleteCashgameUrl>(result.DeleteUrl);
		}

		[Test]
		public void EnableDelete_WithPublishedGame_IsFalse(){
            var cashgame = new CashgameInTest(status: GameStatus.Published);

            var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _locations, null);

			Assert.IsFalse(result.EnableDelete);
		}

		[Test]
		public void EnableDelete_WithFinishedGame_IsTrue(){
            var cashgame = new CashgameInTest(status: GameStatus.Finished);

            var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _locations, null);

			Assert.IsTrue(result.EnableDelete);
		}

        [Test]
		public void LocationSelectModel_IsCorrectType(){
            _locations = new List<string>{"location 1", "location 2", "location 3"};
            var cashgame = new CashgameInTest();

            var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _locations, null);

			Assert.IsInstanceOf<IEnumerable<SelectListItem>>(result.Locations);
        }

        private CashgameEditPageModelFactory GetSut()
        {
            return new CashgameEditPageModelFactory(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<IGlobalization>().Object);
        }
	}
}