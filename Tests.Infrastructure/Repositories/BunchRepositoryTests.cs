using System.Collections.Generic;
using Infrastructure.Data.Cache;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Infrastructure.Repositories
{
    public class BunchRepositoryTests : MockContainer
    {
        [Test]
        public void GetByName_NoBunchFound_ReturnsNull()
        {
            var sut = GetSut();

            var result = sut.GetBySlug("anyname");

            Assert.IsNull(result);
        }

        [Test]
        public void GetByName_BunchFound_ReturnsBunch()
        {
            const string slug = "a";
            const int id = 1;

            var rawBunch = new RawBunch { Slug = slug, TimezoneName = "UTC" };

            GetMock<IBunchStorage>().Setup(o => o.GetIdBySlug(slug)).Returns(id);
            GetMock<IBunchStorage>().Setup(o => o.GetById(id)).Returns(rawBunch);

            var sut = GetSut();
            var result = sut.GetBySlug(slug);

            Assert.AreEqual(result.Slug, slug);
        }

        [Test]
        public void GetByUser_BunchStorageReturnsOneRawBunch_ReturnsOneBunch()
        {
            const int userId = 1;
            var user = new UserInTest(userId);
            var rawBunches = new List<RawBunch> { new RawBunch { TimezoneName = "UTC" } };

            GetMock<IBunchStorage>().Setup(o => o.GetBunchesByUserId(userId)).Returns(rawBunches);

            var sut = GetSut();

            var result = sut.GetByUser(user);

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetAll_NoBunchesInCache_ReturnsTwoBunchesFromDatabase()
        {
            var ids = new List<int> { 1, 2 };
            var homegamesFromDatabase = new List<RawBunch> { new RawBunch { TimezoneName = "UTC" }, new RawBunch { TimezoneName = "UTC" } };
            GetMock<IBunchStorage>().Setup(o => o.GetAllIds()).Returns(ids);
            GetMock<IBunchStorage>().Setup(o => o.GetBunches(ids)).Returns(homegamesFromDatabase);

            var sut = GetSut();

            var result = sut.GetList();

            Assert.AreEqual(2, result.Count);
        }

        // Todo: Maybe move these tests to cache tests
        /*
        [Test]
        public void GetAll_OneBunchInCache_ReturnsTwoBunchesWhereOneIsFromCache()
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
        public void GetAll_TwoBunchesInCache_ReturnsTwoBunchesWhereBothIsFromCache()
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

        private BunchRepository GetSut()
        {
            return new BunchRepository(
                GetMock<IBunchStorage>().Object,
                CacheContainer,
                GetMock<ICacheBuster>().Object);
        }
    }
}