using System.Collections.Generic;
using System.Web.Mvc;
using Core.Classes;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.FormModels;

namespace Web.Tests.ModelFactories{

	public class AddHomegamePageModelFactoryTests
	{

	    private User _user;
		private Homegame _homegame;

        [SetUp]
		public void SetUp(){
            _user = new User();
			_homegame = new Homegame();
		}

		[Test]
        public void DisplayName_IsSet()
		{
		    _homegame.DisplayName = "a";

			var sut = GetSut();
		    var result = sut.Create(_user, _homegame);

			Assert.AreEqual("a", result.DisplayName);
		}

		[Test]
        public void Description_IsSet(){
			_homegame.Description = "a";

			var sut = GetSut();
		    var result = sut.Create(_user, _homegame);

			Assert.AreEqual("a", result.Description);
		}

		[Test]
        public void CurrencySymbol_IsSet(){
			var currency = new CurrencySettings {Symbol = "a"};
		    _homegame.Currency = currency;

			var sut = GetSut();
		    var result = sut.Create(_user, _homegame);

			Assert.AreEqual("a", result.CurrencySymbol);
		}

		[Test]
        public void CurrencyLayoutSelectModel_IsCorrectType(){
			var sut = GetSut();
		    var result = sut.Create(_user, _homegame);

			Assert.IsInstanceOf<List<SelectListItem>>(result.CurrencyLayoutSelectModel);
		}

		[Test]
        public void TimezoneSelectModel_IsCorrectType(){
			var sut = GetSut();
		    var result = sut.Create(_user, _homegame);

            Assert.IsInstanceOf<List<SelectListItem>>(result.TimezoneSelectModel);
		}

		private AddHomegamePageModelFactory GetSut(){
			return new AddHomegamePageModelFactory();
		}

	}

}