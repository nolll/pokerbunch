﻿using System.Collections.Generic;
using System.Net;
using Application.Services;
using Application.UseCases.AppContext;
using Application.UseCases.BaseContext;
using Application.UseCases.BunchContext;
using Core.Entities;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class BunchContextTests : MockContainer
    {
        [Test]
        public void BunchContext_WithSlug_SlugIsSetFromSelectedHomegame()
        {
            const string slug = "a";
            SetupHomegameBySlug(slug);

            var result = GetResult(slug);

            Assert.AreEqual(slug, result.Slug);
            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void BunchContext_WithoutSlug_SlugIsSetFromFirstHomegame()
        {
            var homegameList = ABunchList.WithOneItem().Build();
            SetupHomegameListByUser(homegameList);

            var result = GetResult();

            Assert.AreEqual("a", result.Slug);
            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void BunchContext_WithoutSlugAndHomegames_SlugIsNull()
        {
            var homegameList = ABunchList.Build(); 
            SetupHomegameListByUser(homegameList);

            var result = GetResult();

            Assert.IsNull(result.Slug);
            Assert.IsFalse(result.HasBunch);
        }

        [Test]
        public void Execute_AppContextIsSet()
        {
            const string slug = "a";
            var cashgameContextRequest = new BunchContextRequest(slug);

            var result = Execute(cashgameContextRequest);

            Assert.IsInstanceOf<AppContextResult>(result.AppContext);
        }

        private BunchContextResult Execute(BunchContextRequest request)
        {
            return BunchContextInteractor.Execute(AppContextFunc, GetMock<IBunchRepository>().Object, GetMock<IAuth>().Object, request);
        }

        private AppContextResult AppContextFunc()
        {
            return new AppContextResultInTest();
        }

        private BunchContextResult GetResult(string slug = null)
        {
            var request = new BunchContextRequest(slug);
            return Execute(request);
        }

        private void SetupHomegameBySlug(string slug)
        {
            var homegame = ABunch.Build();
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
        }

        private void SetupHomegameListByUser(IList<Bunch> homegameList)
        {
            GetMock<IBunchRepository>().Setup(o => o.GetByUser(It.IsAny<User>())).Returns(homegameList);
        }
    }
}