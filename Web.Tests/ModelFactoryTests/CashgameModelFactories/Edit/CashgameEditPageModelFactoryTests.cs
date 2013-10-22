using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.Models.CashgameModels.Edit;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Edit{

	public class CashgameEditPageModelFactoryTests : WebMockContainer {

		private User _user;
		private Homegame _homegame;
		private Cashgame _cashgame;
		private List<string> _locations;

        [SetUp]
		public void SetUp(){
			_user = new User();
			_homegame = new Homegame();
			_cashgame = new Cashgame();
			_locations = new List<string>();
		}

        [Test]
		public void IsoDate_IsSet()
        {
            const string formattedStartDate = "a";
            var startTime = DateTime.Parse("2010-01-01 01:00:00");
			_cashgame.StartTime = startTime;

            Mocks.GlobalizationMock.Setup(o => o.FormatIsoDate(startTime)).Returns(formattedStartDate);

			var result = GetResult();

			Assert.AreEqual(formattedStartDate, result.IsoDate);
		}

		[Test]
		public void CancelUrl_IsSet()
		{
		    const string detailsUrl = "a";
		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameDetailsUrl(_homegame, _cashgame)).Returns(detailsUrl);

			var result = GetResult();

			Assert.AreEqual(detailsUrl, result.CancelUrl);
		}

		[Test]
		public void DeleteUrl_IsSet()
        {
            const string deleteUrl = "a";
		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameDeleteUrl(_homegame, _cashgame)).Returns(deleteUrl);
            
            var result = GetResult();

			Assert.AreEqual(deleteUrl, result.DeleteUrl);
		}

		[Test]
		public void EnableDelete_WithPublishedGame_IsFalse(){
			_cashgame.Status = GameStatus.Published;

			var result = GetResult();

			Assert.IsFalse(result.EnableDelete);
		}

		[Test]
		public void EnableDelete_WithFinishedGame_IsTrue(){
			_cashgame.Status = GameStatus.Finished;

			var result = GetResult();

			Assert.IsTrue(result.EnableDelete);
		}

        [Test]
		public void LocationSelectModel_IsCorrectType(){
            _locations = new List<string>{"location 1", "location 2", "location 3"};

			var result = GetResult();

			Assert.IsInstanceOf<IEnumerable<SelectListItem>>(result.Locations);
        }

		private CashgameEditPageModel GetResult(){
			return GetSut().Create(_user, _homegame, _cashgame, _locations, null, null);
		}

        private CashgameEditPageModelFactory GetSut()
        {
            return new CashgameEditPageModelFactory(
                Mocks.PagePropertiesFactoryMock.Object,
                Mocks.UrlProviderMock.Object,
                Mocks.GlobalizationMock.Object);
        }

	}

}