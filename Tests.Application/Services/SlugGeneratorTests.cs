using Application.Services;
using NUnit.Framework;

namespace Tests.Application.Services{

	public class SlugGeneratorTests {

		[Test]
        public void GetSlug_NameWithSpaces_RemovesSpaces()
        {
			var sut = GetSut();
			var result = sut.GetSlug("a b c");

			Assert.AreEqual(result, "abc");
		}

		[Test]
        public void GetSlug_NameWithCaps_RemovesCaps()
        {
			var sut = GetSut();
			var result = sut.GetSlug("AbC");

            Assert.AreEqual(result, "abc");
		}

	    [Test]
	    public void GetSlug_WithNull_ReturnsNull()
	    {
	        var sut = GetSut();
	        var result = sut.GetSlug(null);

            Assert.IsNull(result);
	    }

        private SlugGenerator GetSut()
        {
            return new SlugGenerator();
        }

	}

}