using Core.UseCases;
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

            Assert.AreEqual(TestData.BunchA.DisplayName, result.BunchName);
        }

        [Test]
        public void BunchDetails_DescriptionIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(TestData.BunchA.Description, result.Description);
        }

        [Test]
        public void BunchDetails_HouseRulesIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(TestData.HouseRulesA, result.HouseRules);
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

        private static BunchDetails.Request CreateRequest(string userName = null)
        {
            return new BunchDetails.Request(userName ?? TestData.UserA.UserName, TestData.SlugA);
        }

        private BunchDetails Sut
        {
            get
            {
                return new BunchDetails(
                    Repos.Bunch,
                    Services.UserService,
                    Repos.Player);
            }
        }
    }
}
