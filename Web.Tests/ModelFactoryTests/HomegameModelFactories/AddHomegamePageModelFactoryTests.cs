using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.HomegameModelFactories;
using Web.Models.HomegameModels.Add;

namespace Web.Tests.ModelFactoryTests.HomegameModelFactories{

	public class AddHomegamePageModelFactoryTests : WebMockContainer
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

		    Mocks.GlobalizationMock.Setup(o => o.GetTimezones()).Returns(new List<TimeZoneInfo>());

			var sut = GetSut();
            var result = sut.Create(_user, _postModel);

			Assert.AreEqual("a", result.DisplayName);
		}

		[Test]
        public void Description_WithPostModel_IsSet()
        {
            _postModel.Description = "a";

            Mocks.GlobalizationMock.Setup(o => o.GetTimezones()).Returns(new List<TimeZoneInfo>());

			var sut = GetSut();
            var result = sut.Create(_user, _postModel);

			Assert.AreEqual("a", result.Description);
		}

		[Test]
        public void CurrencySymbol_WithPostModel_IsSet()
        {
		    _postModel.CurrencySymbol = "a";

            Mocks.GlobalizationMock.Setup(o => o.GetTimezones()).Returns(new List<TimeZoneInfo>());

			var sut = GetSut();
		    var result = sut.Create(_user, _postModel);

			Assert.AreEqual("a", result.CurrencySymbol);
		}

		[Test]
        public void CurrencyLayoutSelectModel_IsCorrectType(){
            Mocks.GlobalizationMock.Setup(o => o.GetTimezones()).Returns(new List<TimeZoneInfo>());
            
            var sut = GetSut();
		    var result = sut.Create(_user);

			Assert.IsInstanceOf<List<SelectListItem>>(result.CurrencyLayoutSelectItems);
		}

		[Test]
        public void TimezoneSelectModel_IsCorrectType(){
            Mocks.GlobalizationMock.Setup(o => o.GetTimezones()).Returns(new List<TimeZoneInfo>());
            
            var sut = GetSut();
		    var result = sut.Create(_user);

            Assert.IsInstanceOf<List<SelectListItem>>(result.TimezoneSelectItems);
		}

		private AddHomegamePageModelFactory GetSut(){
            return new AddHomegamePageModelFactory(
                Mocks.PagePropertiesFactoryMock.Object,
                Mocks.GlobalizationMock.Object);
		}

	}

}