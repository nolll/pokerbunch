using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Infrastructure.Tests.Repositories{

	public class 
        HomegameRepositoryTests : MockContainer
	{
        [Test]
		public void GetByName_NoHomegameFound_ReturnsNull()
        {
            var sut = GetSut();

			var result = sut.GetByName("anyname");

			Assert.IsNull(result);
		}

		[Test]
		public void GetByName_HomegameFound_ReturnsHomegame()
		{
		    const string slug = "a";

		    var rawHomegame = new RawHomegame {Slug = slug, TimezoneName = "UTC"};
		    var expectedHomegame = new Homegame {Slug = slug};

		    HomegameStorageMock.Setup(o => o.GetHomegameByName(slug)).Returns(rawHomegame);
		    HomegameFactoryMock.Setup(o => o.Create(rawHomegame)).Returns(expectedHomegame);

		    var sut = GetSut();
            var result = sut.GetByName(slug);

			Assert.AreEqual(result.Slug, slug);
		}

	    [Test]
	    public void GetByUser_HomegameStorageReturnsOneRawHomegame_ReturnsOneHomegame()
	    {
	        const int userId = 1;
	        var user = new User{Id = userId};
	        var rawHomegames = new List<RawHomegame>{new RawHomegame()};

	        HomegameStorageMock.Setup(o => o.GetHomegamesByUserId(userId)).Returns(rawHomegames);
	        HomegameFactoryMock.Setup(o => o.Create(It.IsAny<RawHomegame>())).Returns(new Homegame());

	        var sut = GetSut();

	        var result = sut.GetByUser(user);

            Assert.AreEqual(1, result.Count);
	    }

	    [Test]
	    public void GetAll_NoHomegamesInCache_ReturnsTwoHomegamesFromDatabase()
	    {
	        var slugs = new List<string> {"a", "b"};
            var homegamesFromDatabase = new List<RawHomegame> { new RawHomegame(), new RawHomegame() };
	        HomegameStorageMock.Setup(o => o.GetAllSlugs()).Returns(slugs);
	        HomegameStorageMock.Setup(o => o.GetHomegames(slugs)).Returns(homegamesFromDatabase);
	        HomegameFactoryMock.Setup(o => o.Create(It.IsAny<RawHomegame>())).Returns(new Homegame());

	        var sut = GetSut();

	        var result = sut.GetAll();

            Assert.AreEqual(2, result.Count);
	    }

        [Test]
        public void GetAll_OneHomegameInCache_ReturnsTwoHomegamesWhereOneIsFromCache()
        {
            const string slugA = "a";
            const string slugB = "b";
            const string cacheKeyA = "cachekeyA";
            const string cacheKeyB = "cachekeyB";
            var slugs = new List<string> { slugA, slugB };
            var homegamesFromDatabase = new List<RawHomegame> { new RawHomegame() };
            var homegameFromCache = new Homegame();
            HomegameStorageMock.Setup(o => o.GetAllSlugs()).Returns(slugs);
            HomegameStorageMock.Setup(o => o.GetHomegames(It.IsAny<List<string>>())).Returns(homegamesFromDatabase);
            CacheContainerMock.Setup(o => o.ConstructCacheKey(It.IsAny<string>(), slugA)).Returns(cacheKeyA);
            CacheContainerMock.Setup(o => o.ConstructCacheKey(It.IsAny<string>(), slugB)).Returns(cacheKeyB);
            CacheContainerMock.Setup(o => o.Get<Homegame>(cacheKeyA)).Returns(homegameFromCache);
            CacheContainerMock.Setup(o => o.Get<Homegame>(cacheKeyB)).Returns((Homegame)null);
            HomegameFactoryMock.Setup(o => o.Create(It.IsAny<RawHomegame>())).Returns(new Homegame());

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
            var homegameFromCache = new Homegame();
            HomegameStorageMock.Setup(o => o.GetAllSlugs()).Returns(slugs);
            HomegameStorageMock.Setup(o => o.GetHomegames(It.IsAny<List<string>>())).Returns(homegamesFromDatabase);
            CacheContainerMock.Setup(o => o.ConstructCacheKey(It.IsAny<string>(), slugA)).Returns(cacheKeyA);
            CacheContainerMock.Setup(o => o.ConstructCacheKey(It.IsAny<string>(), slugB)).Returns(cacheKeyB);
            CacheContainerMock.Setup(o => o.Get<Homegame>(cacheKeyA)).Returns(homegameFromCache);
            CacheContainerMock.Setup(o => o.Get<Homegame>(cacheKeyB)).Returns(homegameFromCache);
            HomegameFactoryMock.Setup(o => o.Create(It.IsAny<RawHomegame>())).Returns(new Homegame());

            var sut = GetSut();

            var result = sut.GetAll();

            Assert.AreEqual(2, result.Count);
        }

        private HomegameRepository GetSut()
        {
            return new HomegameRepository(HomegameStorageMock.Object, HomegameFactoryMock.Object, CacheContainerMock.Object, RawHomegameFactoryMock.Object);
        }

	}

}