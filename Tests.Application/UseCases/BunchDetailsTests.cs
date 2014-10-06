using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using Core.UseCases.BunchDetails;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class BunchDetailsTests : TestBase
    {
        private const string Slug = "a";
        private const string BunchName = "b";
        private const string Description = "c";
        private const string HouseRules = "d";

        [Test]
        public void BunchDetails_BunchNameIsSet()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.AreEqual(BunchName, result.BunchName);
        }

        [Test]
        public void BunchDetails_DescriptionIsSet()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.AreEqual(Description, result.Description);
        }

        [Test]
        public void BunchDetails_HouseRulesIsSet()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.AreEqual(HouseRules, result.HouseRules);
        }

        [Test]
        public void BunchDetails_EditBunchUrlIsSet()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<EditBunchUrl>(result.EditBunchUrl);
        }

        [Test]
        public void BunchDetails_WithPlayer_CanEditIsFalse()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.IsFalse(result.CanEdit);
        }

        [Test]
        public void BunchDetails_WithManager_CanEditIsTrue()
        {
            SetupBunch();
            SetupManager();

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.CanEdit);
        }

        private void SetupManager()
        {
            GetMock<IAuth>().Setup(o => o.IsInRole(Slug, Role.Manager)).Returns(true);
        }

        private void SetupBunch()
        {
            var bunch = new BunchInTest(slug: Slug, displayName: BunchName, description: Description, houseRules: HouseRules);
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(bunch);
        }

        private static BunchDetailsRequest CreateRequest()
        {
            return new BunchDetailsRequest(Slug);
        }

        private BunchDetailsResult Execute(BunchDetailsRequest request)
        {
            return BunchDetailsInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                GetMock<IAuth>().Object,
                request);
        }
    }
}
