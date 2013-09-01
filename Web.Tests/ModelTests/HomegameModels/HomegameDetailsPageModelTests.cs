using Core.Classes;
using NUnit.Framework;
using Web.Models.HomegameModels.Details;
using Web.Models.UrlModels;

namespace Web.Tests.ModelTests.HomegameModels{

	public class HomegameDetailsPageModelTests {

		private User _user;
		private Homegame _homegame;
		private bool _isInManagerMode;

        [SetUp]
		public void SetUp(){
			_user = new User();
			_homegame = new Homegame();
			_isInManagerMode = false;
		}

        [Test]
		public void ActionDetails_SetsDisplayName(){
			_homegame.DisplayName = "a";

			var sut = GetSut();

			Assert.AreEqual("a", sut.DisplayName);
		}

		[Test]
		public void ActionDetails_SetsHouseRules(){
			_homegame.HouseRules = "a";

			var sut = GetSut();

			Assert.AreEqual("a", sut.HouseRules);
		}

		[Test]
		public void ActionDetails_HouseRulesWithLineBreaks_OutputsBrTags(){
			_homegame.HouseRules = "a\n\rb";

			var sut = GetSut();

			Assert.AreEqual("a<br />\n\rb", sut.HouseRules);
		}

		[Test]
		public void ActionDetails_SetsEditUrl(){
			var sut = GetSut();

			Assert.IsInstanceOf<HomegameEditUrlModel>(sut.EditUrl);
		}

		[Test]
		public void ActionDetails_SetsDescription(){
			_homegame.Description = "a";

			var sut = GetSut();

			Assert.AreEqual("a", sut.Description);
		}

		[Test]
		public void ActionDetails_WithPlayerRights_DoesNotOutputEditLink(){
			var sut = GetSut();

			Assert.IsFalse(sut.ShowEditLink);
		}

		[Test]
		public void ActionDetails_WithManagerRights_ShowsEditLink(){
			_isInManagerMode = true;

			var sut = GetSut();

			Assert.IsTrue(sut.ShowEditLink);
		}

		private HomegameDetailsPageModel GetSut(){
			return new HomegameDetailsPageModel(_user, _homegame, _isInManagerMode);
		}

	}

}