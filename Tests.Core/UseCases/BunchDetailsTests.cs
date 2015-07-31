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
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(TestData.BunchNameA, result.BunchName);
        }

        [Test]
        public void BunchDetails_DescriptionIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(TestData.DescriptionA, result.Description);
        }

        [Test]
        public void BunchDetails_HouseRulesIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(TestData.HouseRulesA, result.HouseRules);
        }

        [Test]
        public void BunchDetails_EditBunchUrlIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.IsInstanceOf<EditBunchUrl>(result.EditBunchUrl);
        }

        [Test]
        public void BunchDetails_WithPlayer_CanEditIsFalse()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.IsFalse(result.CanEdit);
        }

        [Test]
        public void BunchDetails_WithManager_CanEditIsTrue()
        {
            var result = Sut.Execute(CreateRequest(TestData.UserNameC));

            Assert.IsTrue(result.CanEdit);
        }

        private static BunchDetailsRequest CreateRequest(string userName = TestData.UserNameA)
        {
            return new BunchDetailsRequest(TestData.SlugA, userName);
        }

        private BunchDetailsInteractor Sut
        {
            get
            {
                return new BunchDetailsInteractor(
                    Repos.Bunch,
                    Repos.User,
                    Repos.Player);
            }
        }
    }
}
