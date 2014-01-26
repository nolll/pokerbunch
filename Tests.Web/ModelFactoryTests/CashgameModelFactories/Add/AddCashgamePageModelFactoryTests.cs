using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Add;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Add;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Add{

	public class AddCashgamePageModelFactoryTests : MockContainer {

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
        public void Location_WithPostModel_IsSet()
		{
		    var postModel = new AddCashgamePostModel {TypedLocation = "a"};
			
            var result = GetResult(postModel);

			Assert.AreEqual(result.TypedLocation, "a");
		}

		[Test]
        public void Location_WithoutPostModel_IsNull()
		{
            var result = GetResult();

			Assert.IsNull(result.Location);
		}

		[Test]
        public void LocationSelectModel_IsCorrectLength(){
            _locations = new List<string>{ "a" };

			var result = GetResult();

			Assert.AreEqual(2, result.Locations.Count());
		}

		private AddCashgamePageModel GetResult(AddCashgamePostModel postModel = null)
		{
		    var sut = GetSut();
            return sut.Create(_user, _homegame, _locations, postModel);
		}

        private AddCashgamePageModelFactory GetSut()
        {
            return new AddCashgamePageModelFactory(GetMock<IPagePropertiesFactory>().Object);
        }

	}

}