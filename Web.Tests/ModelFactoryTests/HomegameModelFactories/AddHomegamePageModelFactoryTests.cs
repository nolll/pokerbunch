using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Core.Classes;
using Infrastructure.System;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Add;

namespace Web.Tests.ModelFactoryTests.HomegameModelFactories{

	public class AddHomegamePageModelFactoryTests : MockContainer
	{
	    private User _user;
		private AddHomegamePostModel _postModel;

        [SetUp]
		public void SetUp(){
            _user = new FakeUser();
			_postModel = new AddHomegamePostModel();
		}

		[Test]
        public void DisplayName_WithPostModel_IsSet()
		{
		    _postModel.DisplayName = "a";

		    GetMock<IGlobalization>().Setup(o => o.GetTimezones()).Returns(new List<TimeZoneInfo>());

			var sut = GetSut();
            var result = sut.Create(_user, _postModel);

			Assert.AreEqual("a", result.DisplayName);
		}

		[Test]
        public void Description_WithPostModel_IsSet()
        {
            _postModel.Description = "a";

            GetMock<IGlobalization>().Setup(o => o.GetTimezones()).Returns(new List<TimeZoneInfo>());

			var sut = GetSut();
            var result = sut.Create(_user, _postModel);

			Assert.AreEqual("a", result.Description);
		}

		[Test]
        public void CurrencySymbol_WithPostModel_IsSet()
        {
		    _postModel.CurrencySymbol = "a";

            GetMock<IGlobalization>().Setup(o => o.GetTimezones()).Returns(new List<TimeZoneInfo>());

			var sut = GetSut();
		    var result = sut.Create(_user, _postModel);

			Assert.AreEqual("a", result.CurrencySymbol);
		}

		[Test]
        public void CurrencyLayoutSelectModel_IsCorrectType(){
            GetMock<IGlobalization>().Setup(o => o.GetTimezones()).Returns(new List<TimeZoneInfo>());
            
            var sut = GetSut();
		    var result = sut.Create(_user);

			Assert.IsInstanceOf<List<SelectListItem>>(result.CurrencyLayoutSelectItems);
		}

		[Test]
        public void TimezoneSelectModel_IsCorrectType(){
            GetMock<IGlobalization>().Setup(o => o.GetTimezones()).Returns(new List<TimeZoneInfo>());
            
            var sut = GetSut();
		    var result = sut.Create(_user);

            Assert.IsInstanceOf<List<SelectListItem>>(result.TimezoneSelectItems);
		}

		private AddHomegamePageModelFactory GetSut(){
            return new AddHomegamePageModelFactory(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<IGlobalization>().Object);
		}

	}

}