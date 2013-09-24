using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories;
using Web.Models.CashgameModels.Edit;
using Web.Models.UrlModels;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories{

	public class CashgameEditPageModelFactoryTests : MockContainer {

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
		public void IsoDate_IsSet(){
			_cashgame.StartTime = DateTime.Parse("2010-01-01 01:00:00");

			var result = GetResult();

			Assert.AreEqual("2010-01-01", result.IsoDate);
		}

		[Test]
		public void CancelUrl_IsSet(){
			var result = GetResult();

			Assert.IsInstanceOf<CashgameDetailsUrlModel>(result.CancelUrl);
		}

		[Test]
		public void DeleteUrl_IsSet(){
			var result = GetResult();

			Assert.IsInstanceOf<CashgameDeleteUrlModel>(result.DeleteUrl);
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
            return new CashgameEditPageModelFactory(PagePropertiesFactoryMock.Object);
        }

	}

}