using Core.Entities;
using Core.Services;
using Core.Urls;
using Core.UseCases.BunchDetails;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class BunchDetailsTests : TestBase
    {
        [Test]
        public void BunchDetails_BunchNameIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(Constants.BunchNameA, result.BunchName);
        }

        [Test]
        public void BunchDetails_DescriptionIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(Constants.DescriptionA, result.Description);
        }

        [Test]
        public void BunchDetails_HouseRulesIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(Constants.HouseRulesA, result.HouseRules);
        }

        [Test]
        public void BunchDetails_EditBunchUrlIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<EditBunchUrl>(result.EditBunchUrl);
        }

        [Test]
        public void BunchDetails_WithPlayer_CanEditIsFalse()
        {
            var result = Execute(CreateRequest());

            Assert.IsFalse(result.CanEdit);
        }

        [Test]
        public void BunchDetails_WithManager_CanEditIsTrue()
        {
            SetupManager();

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.CanEdit);
        }

        private void SetupManager()
        {
            GetMock<IAuth>().Setup(o => o.IsInRole(Constants.SlugA, Role.Manager)).Returns(true);
        }

        private static BunchDetailsRequest CreateRequest()
        {
            return new BunchDetailsRequest(Constants.SlugA);
        }

        private BunchDetailsResult Execute(BunchDetailsRequest request)
        {
            return BunchDetailsInteractor.Execute(
                Repo.Bunch,
                GetMock<IAuth>().Object,
                request);
        }
    }
}
