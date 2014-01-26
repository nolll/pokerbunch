using System;
using System.Collections.Generic;
using System.Web.Mvc;
using App.Services.Interfaces;
using Core.Classes;
using Infrastructure.System;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.ModelFactories.PageBaseModelFactories;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Edit{

	public class CashgameEditPageModelFactoryTests : MockContainer {

		private User _user;
		private Homegame _homegame;
		private List<string> _locations;

        [SetUp]
		public void SetUp(){
            _user = new FakeUser();
			_homegame = new FakeHomegame();
			_locations = new List<string>();
		}

        [Test]
		public void IsoDate_IsSet()
        {
            const string formattedStartDate = "a";
            var startTime = DateTime.Parse("2010-01-01 01:00:00");
            var cashgame = new FakeCashgame(startTime: startTime);

            GetMock<IGlobalization>().Setup(o => o.FormatIsoDate(startTime)).Returns(formattedStartDate);

            var sut = GetSut();
            var result = sut.Create(_user, _homegame, cashgame, _locations, null);

			Assert.AreEqual(formattedStartDate, result.IsoDate);
		}

		[Test]
		public void CancelUrl_IsSet()
		{
		    const string detailsUrl = "a";
            var cashgame = new FakeCashgame();
		    GetMock<IUrlProvider>().Setup(o => o.GetCashgameDetailsUrl(_homegame.Slug, cashgame.DateString)).Returns(detailsUrl);

            var sut = GetSut();
            var result = sut.Create(_user, _homegame, cashgame, _locations, null);

			Assert.AreEqual(detailsUrl, result.CancelUrl);
		}

		[Test]
		public void DeleteUrl_IsSet()
        {
            const string deleteUrl = "a";
            var cashgame = new FakeCashgame();
		    GetMock<IUrlProvider>().Setup(o => o.GetCashgameDeleteUrl(_homegame.Slug, cashgame.DateString)).Returns(deleteUrl);

            var sut = GetSut();
            var result = sut.Create(_user, _homegame, cashgame, _locations, null);

			Assert.AreEqual(deleteUrl, result.DeleteUrl);
		}

		[Test]
		public void EnableDelete_WithPublishedGame_IsFalse(){
            var cashgame = new FakeCashgame(status: GameStatus.Published);

            var sut = GetSut();
            var result = sut.Create(_user, _homegame, cashgame, _locations, null);

			Assert.IsFalse(result.EnableDelete);
		}

		[Test]
		public void EnableDelete_WithFinishedGame_IsTrue(){
            var cashgame = new FakeCashgame(status: GameStatus.Finished);

            var sut = GetSut();
            var result = sut.Create(_user, _homegame, cashgame, _locations, null);

			Assert.IsTrue(result.EnableDelete);
		}

        [Test]
		public void LocationSelectModel_IsCorrectType(){
            _locations = new List<string>{"location 1", "location 2", "location 3"};
            var cashgame = new FakeCashgame();

            var sut = GetSut();
            var result = sut.Create(_user, _homegame, cashgame, _locations, null);

			Assert.IsInstanceOf<IEnumerable<SelectListItem>>(result.Locations);
        }

        private CashgameEditPageModelFactory GetSut()
        {
            return new CashgameEditPageModelFactory(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<IUrlProvider>().Object,
                GetMock<IGlobalization>().Object);
        }

	}

}