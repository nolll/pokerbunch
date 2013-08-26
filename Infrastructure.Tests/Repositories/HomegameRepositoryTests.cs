using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace tes{

	public class HomegameRepositoryTests : MockContainer
	{
        [SetUp]
		public void SetUp(){
            HomegameStorageMock = new Mock<IHomegameStorage>();
		}

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

        private HomegameRepository GetSut()
        {
            return new HomegameRepository(HomegameStorageMock.Object, HomegameFactoryMock.Object, CacheRepositoryFake);
        }

	}

}