using Application.Services;
using Application.Urls;
using Application.UseCases.BunchContext;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.HomegameModelFactories;
using Web.Models.UrlModels;

namespace Tests.Web.ModelFactoryTests.HomegameModelFactories
{
	public class HomegameDetailsPageBuilderTests : MockContainer
    {
		private bool _isInManagerMode;

        [SetUp]
		public void SetUp(){
			_isInManagerMode = false;
		}

        [Test]
		public void Create_SetsDisplayName()
        {
            const string displayName = "a";
            var homegame = new HomegameInTest(displayName: displayName);
            var bunchContextResult = new BunchContextResultInTest();

			var sut = GetSut();
            var result = sut.Create(bunchContextResult, homegame, _isInManagerMode);

			Assert.AreEqual(displayName, result.DisplayName);
		}

		[Test]
        public void Create_SetsHouseRules()
		{
		    const string houseRules = "a";
            var homegame = new HomegameInTest(houseRules: houseRules);
            var bunchContextResult = new BunchContextResultInTest();

			var sut = GetSut();
            var result = sut.Create(bunchContextResult, homegame, _isInManagerMode);

			Assert.AreEqual(houseRules, result.HouseRules);
		}

		[Test]
        public void Create_HouseRulesWithLineBreaks_OutputsBrTags()
		{
            const string houseRules = "a\n\rb";
            const string formattedHouseRules = "a<br />\n\rb";
            var homegame = new HomegameInTest(houseRules: houseRules);
            var bunchContextResult = new BunchContextResultInTest();

			var sut = GetSut();
            var result = sut.Create(bunchContextResult, homegame, _isInManagerMode);

			Assert.AreEqual(formattedHouseRules, result.HouseRules);
		}

		[Test]
        public void Create_SetsEditUrl()
		{
		    var homegame = new HomegameInTest();
            var bunchContextResult = new BunchContextResultInTest();

			var sut = GetSut();
            var result = sut.Create(bunchContextResult, homegame, _isInManagerMode);

			Assert.IsInstanceOf<EditHomegameUrl>(result.EditUrl);
		}

		[Test]
        public void Create_SetsDescription()
		{
		    const string description = "a";
            var homegame = new HomegameInTest(description: description);
            var bunchContextResult = new BunchContextResultInTest();

			var sut = GetSut();
            var result = sut.Create(bunchContextResult, homegame, _isInManagerMode);

			Assert.AreEqual(description, result.Description);
		}

		[Test]
        public void Create_WithPlayerRights_DoesNotOutputEditLink()
		{
		    var homegame = new HomegameInTest();
            var bunchContextResult = new BunchContextResultInTest();

			var sut = GetSut();
            var result = sut.Create(bunchContextResult, homegame, _isInManagerMode);

			Assert.IsFalse(result.ShowEditLink);
		}

		[Test]
        public void Create_WithManagerRights_ShowsEditLink()
        {
            var homegame = new HomegameInTest();
            _isInManagerMode = true;
            var bunchContextResult = new BunchContextResultInTest();

			var sut = GetSut();
            var result = sut.Create(bunchContextResult, homegame, _isInManagerMode);

			Assert.IsTrue(result.ShowEditLink);
		}

		private HomegameDetailsPageBuilder GetSut(){
            return new HomegameDetailsPageBuilder(
                GetMock<IHomegameRepository>().Object,
                GetMock<IAuth>().Object,
                GetMock<IBunchContextInteractor>().Object);
		}
	}
}