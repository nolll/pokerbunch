using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.HomegameModelFactories;

namespace Web.Tests.ModelFactoryTests.HomegameModelFactories{

	public class HomegameDetailsPageModelFactoryTests : WebMockContainer
    {

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
		public void Create_SetsDisplayName(){
			_homegame.DisplayName = "a";

			var sut = GetSut();
            var result = sut.Create(_user, _homegame, _isInManagerMode);

			Assert.AreEqual("a", result.DisplayName);
		}

		[Test]
        public void Create_SetsHouseRules()
        {
			_homegame.HouseRules = "a";

			var sut = GetSut();
            var result = sut.Create(_user, _homegame, _isInManagerMode);

			Assert.AreEqual("a", result.HouseRules);
		}

		[Test]
        public void Create_HouseRulesWithLineBreaks_OutputsBrTags()
        {
			_homegame.HouseRules = "a\n\rb";

			var sut = GetSut();
            var result = sut.Create(_user, _homegame, _isInManagerMode);

			Assert.AreEqual("a<br />\n\rb", result.HouseRules);
		}

		[Test]
        public void Create_SetsEditUrl()
		{
		    const string editUrl = "a";
		    Mocks.UrlProviderMock.Setup(o => o.GetHomegameEditUrl(_homegame)).Returns(editUrl);

			var sut = GetSut();
            var result = sut.Create(_user, _homegame, _isInManagerMode);

			Assert.AreEqual(editUrl, result.EditUrl);
		}

		[Test]
        public void Create_SetsDescription()
        {
			_homegame.Description = "a";

			var sut = GetSut();
            var result = sut.Create(_user, _homegame, _isInManagerMode);

			Assert.AreEqual("a", result.Description);
		}

		[Test]
        public void Create_WithPlayerRights_DoesNotOutputEditLink()
        {
			var sut = GetSut();
            var result = sut.Create(_user, _homegame, _isInManagerMode);

			Assert.IsFalse(result.ShowEditLink);
		}

		[Test]
        public void Create_WithManagerRights_ShowsEditLink()
        {
			_isInManagerMode = true;

			var sut = GetSut();
            var result = sut.Create(_user, _homegame, _isInManagerMode);

			Assert.IsTrue(result.ShowEditLink);
		}

		private HomegameDetailsPageModelFactory GetSut(){
            return new HomegameDetailsPageModelFactory(
                Mocks.PagePropertiesFactoryMock.Object,
                Mocks.UrlProviderMock.Object);
		}

	}

}