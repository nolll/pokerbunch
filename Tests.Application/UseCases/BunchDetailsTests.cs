﻿using Application.Services;
using Application.Urls;
using Application.UseCases.BunchDetails;
using Core.Entities;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class BunchDetailsTests : MockContainer
    {
        private const string Slug = "a";
        private const string BunchName = "b";
        private const string Description = "c";
        private const string HouseRules = "d";

        [Test]
        public void BunchDetails_BunchNameIsSet()
        {
            SetupBunch();

            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(BunchName, result.BunchName);
        }

        [Test]
        public void BunchDetails_DescriptionIsSet()
        {
            SetupBunch();

            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(Description, result.Description);
        }

        [Test]
        public void BunchDetails_HouseRulesIsSet()
        {
            SetupBunch();

            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(HouseRules, result.HouseRules);
        }

        [Test]
        public void BunchDetails_EditBunchUrlIsSet()
        {
            SetupBunch();

            var result = Sut.Execute(CreateRequest());

            Assert.IsInstanceOf<EditBunchUrl>(result.EditBunchUrl);
        }

        [Test]
        public void BunchDetails_WithPlayer_CanEditIsFalse()
        {
            SetupBunch();

            var result = Sut.Execute(CreateRequest());

            Assert.IsFalse(result.CanEdit);
        }

        [Test]
        public void BunchDetails_WithManager_CanEditIsTrue()
        {
            SetupBunch();
            SetupManager();

            var result = Sut.Execute(CreateRequest());

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

        private BunchDetailsInteractor Sut
        {
            get
            {
                return new BunchDetailsInteractor(
                    GetMock<IBunchRepository>().Object,
                    GetMock<IAuth>().Object);
            }
        }
    }
}