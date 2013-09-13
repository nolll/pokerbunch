using System.Collections.Generic;
using System.Web.Mvc;
using Core.Classes;
using NUnit.Framework;
using Web.ModelFactories.HomegameModelFactories;
using Web.Models.HomegameModels.Add;

namespace Web.Tests.ModelFactories{

	public class AddHomegamePageModelFactoryTests
	{

	    private User _user;
		private AddHomegamePostModel _postModel;

        [SetUp]
		public void SetUp(){
            _user = new User();
			_postModel = new AddHomegamePostModel();
		}

		[Test]
        public void DisplayName_WithPostModel_IsSet()
		{
		    _postModel.DisplayName = "a";

			var sut = GetSut();
            var result = sut.Create(_user, _postModel);

			Assert.AreEqual("a", result.DisplayName);
		}

		[Test]
        public void Description_WithPostModel_IsSet()
        {
            _postModel.Description = "a";

			var sut = GetSut();
            var result = sut.Create(_user, _postModel);

			Assert.AreEqual("a", result.Description);
		}

		[Test]
        public void CurrencySymbol_WithPostModel_IsSet()
        {
		    _postModel.CurrencySymbol = "a";

			var sut = GetSut();
		    var result = sut.Create(_user, _postModel);

			Assert.AreEqual("a", result.CurrencySymbol);
		}

		[Test]
        public void CurrencyLayoutSelectModel_IsCorrectType(){
			var sut = GetSut();
		    var result = sut.Create(_user);

			Assert.IsInstanceOf<List<SelectListItem>>(result.CurrencyLayoutSelectModel);
		}

		[Test]
        public void TimezoneSelectModel_IsCorrectType(){
			var sut = GetSut();
		    var result = sut.Create(_user);

            Assert.IsInstanceOf<List<SelectListItem>>(result.TimezoneSelectModel);
		}

		private AddHomegamePageModelFactory GetSut(){
			return new AddHomegamePageModelFactory();
		}

	}

}