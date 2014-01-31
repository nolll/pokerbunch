using Application.Services;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelFactories.PageBaseModelFactories;

namespace Tests.Web.ModelFactoryTests.HomegameModelFactories{

	public class HomegameDetailsPageModelFactoryTests : MockContainer
    {

		private User _user;
		private bool _isInManagerMode;

        [SetUp]
		public void SetUp(){
            _user = new FakeUser();
			_isInManagerMode = false;
		}

        [Test]
		public void Create_SetsDisplayName()
        {
            const string displayName = "a";
            var homegame = new FakeHomegame(displayName: displayName);

			var sut = GetSut();
            var result = sut.Create(_user, homegame, _isInManagerMode);

			Assert.AreEqual(displayName, result.DisplayName);
		}

		[Test]
        public void Create_SetsHouseRules()
		{
		    const string houseRules = "a";
            var homegame = new FakeHomegame(houseRules: houseRules);

			var sut = GetSut();
            var result = sut.Create(_user, homegame, _isInManagerMode);

			Assert.AreEqual(houseRules, result.HouseRules);
		}

		[Test]
        public void Create_HouseRulesWithLineBreaks_OutputsBrTags()
		{
            const string houseRules = "a\n\rb";
            const string formattedHouseRules = "a<br />\n\rb";
            var homegame = new FakeHomegame(houseRules: houseRules);

			var sut = GetSut();
            var result = sut.Create(_user, homegame, _isInManagerMode);

			Assert.AreEqual(formattedHouseRules, result.HouseRules);
		}

		[Test]
        public void Create_SetsEditUrl()
		{
		    const string editUrl = "a";
		    var homegame = new FakeHomegame();
		    GetMock<IUrlProvider>().Setup(o => o.GetHomegameEditUrl(homegame.Slug)).Returns(editUrl);

			var sut = GetSut();
            var result = sut.Create(_user, homegame, _isInManagerMode);

			Assert.AreEqual(editUrl, result.EditUrl);
		}

		[Test]
        public void Create_SetsDescription()
		{
		    const string description = "a";
            var homegame = new FakeHomegame(description: description);

			var sut = GetSut();
            var result = sut.Create(_user, homegame, _isInManagerMode);

			Assert.AreEqual(description, result.Description);
		}

		[Test]
        public void Create_WithPlayerRights_DoesNotOutputEditLink()
		{
		    var homegame = new FakeHomegame();

			var sut = GetSut();
            var result = sut.Create(_user, homegame, _isInManagerMode);

			Assert.IsFalse(result.ShowEditLink);
		}

		[Test]
        public void Create_WithManagerRights_ShowsEditLink()
        {
            var homegame = new FakeHomegame();
            _isInManagerMode = true;

			var sut = GetSut();
            var result = sut.Create(_user, homegame, _isInManagerMode);

			Assert.IsTrue(result.ShowEditLink);
		}

		private HomegameDetailsPageModelFactory GetSut(){
            return new HomegameDetailsPageModelFactory(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<IUrlProvider>().Object);
		}

	}

}