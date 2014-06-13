using System.Collections.Generic;
using Infrastructure.Data.Cache;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Mappers;
using Infrastructure.Data.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Infrastructure.Repositories
{
    public class HomegameRepositoryTests : MockContainer
    {
        [Test]
        public void GetByName_NoHomegameFound_ReturnsNull()
        {
            var sut = GetSut();

            var result = sut.GetBySlug("anyname");

            Assert.IsNull(result);
        }

        [Test]
        public void GetByName_HomegameFound_ReturnsHomegame()
        {
            const string slug = "a";
            const int id = 1;

            var rawHomegame = new RawHomegame { Slug = slug, TimezoneName = "UTC" };
            var expectedHomegame = new HomegameInTest(slug: slug);

            GetMock<IHomegameStorage>().Setup(o => o.GetIdBySlug(slug)).Returns(id);
            GetMock<IHomegameStorage>().Setup(o => o.GetById(id)).Returns(rawHomegame);
            GetMock<IHomegameDataMapper>().Setup(o => o.Map(rawHomegame)).Returns(expectedHomegame);

            var sut = GetSut();
            var result = sut.GetBySlug(slug);

            Assert.AreEqual(result.Slug, slug);
        }

        [Test]
        public void GetByUser_HomegameStorageReturnsOneRawHomegame_ReturnsOneHomegame()
        {
            const int userId = 1;
            var user = new UserInTest(userId);
            var rawHomegames = new List<RawHomegame> { new RawHomegame() };

            GetMock<IHomegameStorage>().Setup(o => o.GetHomegamesByUserId(userId)).Returns(rawHomegames);
            GetMock<IHomegameDataMapper>().Setup(o => o.Map(It.IsAny<RawHomegame>())).Returns(new HomegameInTest());

            var sut = GetSut();

            var result = sut.GetByUser(user);

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetAll_NoHomegamesInCache_ReturnsTwoHomegamesFromDatabase()
        {
            var ids = new List<int> { 1, 2 };
            var homegamesFromDatabase = new List<RawHomegame> { new RawHomegame(), new RawHomegame() };
            GetMock<IHomegameStorage>().Setup(o => o.GetAllIds()).Returns(ids);
            GetMock<IHomegameStorage>().Setup(o => o.GetHomegames(ids)).Returns(homegamesFromDatabase);
            GetMock<IHomegameDataMapper>().Setup(o => o.Map(It.IsAny<RawHomegame>())).Returns(new HomegameInTest());

            var sut = GetSut();

            var result = sut.GetList();

            Assert.AreEqual(2, result.Count);
        }

        // Todo: Maybe move these tests to cache tests
        /*
        [Test]
        public void GetAll_OneHomegameInCache_ReturnsTwoHomegamesWhereOneIsFromCache()
        {
            const string slugA = "a";
            const string slugB = "b";
            const string cacheKeyA = "cachekeyA";
            const string cacheKeyB = "cachekeyB";
            var slugs = new List<string> { slugA, slugB };
            var homegamesFromDatabase = new List<RawHomegame> { new RawHomegame() };
            var homegameFromCache = new FakeHomegame();
            GetMock<IHomegameStorage>().Setup(o => o.GetAllSlugs()).Returns(slugs);
            GetMock<IHomegameStorage>().Setup(o => o.GetHomegames(It.IsAny<List<string>>())).Returns(homegamesFromDatabase);
            GetMock<ICacheContainer>().Setup(o => o.ConstructCacheKey(It.IsAny<string>(), slugA)).Returns(cacheKeyA);
            GetMock<ICacheContainer>().Setup(o => o.ConstructCacheKey(It.IsAny<string>(), slugB)).Returns(cacheKeyB);
            GetMock<ICacheContainer>().Setup(o => o.Get<Homegame>(cacheKeyA)).Returns(homegameFromCache);
            GetMock<ICacheContainer>().Setup(o => o.Get<Homegame>(cacheKeyB)).Returns((Homegame)null);
            GetMock<IHomegameFactory>().Setup(o => o.Create(It.IsAny<RawHomegame>())).Returns(new FakeHomegame());

            var sut = GetSut();

            var result = sut.GetAll();

            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void GetAll_TwoHomegamesInCache_ReturnsTwoHomegamesWhereBothIsFromCache()
        {
            const string slugA = "a";
            const string slugB = "b";
            const string cacheKeyA = "cachekeyA";
            const string cacheKeyB = "cachekeyB";
            var slugs = new List<string> { slugA, slugB };
            var homegamesFromDatabase = new List<RawHomegame> { };
            var homegameFromCache = new FakeHomegame();
            GetMock<IHomegameStorage>().Setup(o => o.GetAllSlugs()).Returns(slugs);
            GetMock<IHomegameStorage>().Setup(o => o.GetHomegames(It.IsAny<List<string>>())).Returns(homegamesFromDatabase);
            GetMock<ICacheContainer>().Setup(o => o.ConstructCacheKey(It.IsAny<string>(), slugA)).Returns(cacheKeyA);
            GetMock<ICacheContainer>().Setup(o => o.ConstructCacheKey(It.IsAny<string>(), slugB)).Returns(cacheKeyB);
            GetMock<ICacheContainer>().Setup(o => o.Get<Homegame>(cacheKeyA)).Returns(homegameFromCache);
            GetMock<ICacheContainer>().Setup(o => o.Get<Homegame>(cacheKeyB)).Returns(homegameFromCache);
            GetMock<IHomegameFactory>().Setup(o => o.Create(It.IsAny<RawHomegame>())).Returns(new FakeHomegame());

            var sut = GetSut();

            var result = sut.GetAll();

            Assert.AreEqual(2, result.Count);
        }
        */

        private HomegameRepository GetSut()
        {
            return new HomegameRepository(
                GetMock<IHomegameStorage>().Object,
                CacheContainer,
                GetMock<ICacheKeyProvider>().Object,
                GetMock<ICacheBuster>().Object,
                GetMock<IRawHomegameFactory>().Object,
                GetMock<IHomegameDataMapper>().Object);
        }
    }
}