using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Repositories;
using Moq;
using NUnit.Framework;

namespace tes{

	public class HomegameRepositoryTests
	{
	    private Mock<IHomegameStorage> _homegameStorageMock;

        [SetUp]
		public void SetUp(){
            _homegameStorageMock = new Mock<IHomegameStorage>();
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
		    var slug = "a";

			var rawHomegame = new RawHomegame();
		    rawHomegame.Slug = slug;
			rawHomegame.TimezoneName = "UTC";

		    _homegameStorageMock.Setup(o => o.GetHomegameByName(slug)).Returns(rawHomegame);

		    var sut = GetSut();
            var result = sut.GetByName(slug);

			Assert.AreEqual(result.Slug, slug);
		}

        private HomegameRepository GetSut()
        {
            return new HomegameRepository(_homegameStorageMock.Object);
        }

	}

}