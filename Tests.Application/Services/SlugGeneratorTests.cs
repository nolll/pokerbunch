using Application.Services;
using NUnit.Framework;

namespace Tests.Application.Services
{
	public class SlugGeneratorTests
    {
		[Test]
        public void GetSlug_NameWithSpaces_RemovesSpaces()
        {
			var result = SlugGenerator.GetSlug("a b c");

			Assert.AreEqual(result, "abc");
		}

		[Test]
        public void GetSlug_NameWithCaps_RemovesCaps()
        {
            var result = SlugGenerator.GetSlug("AbC");

            Assert.AreEqual(result, "abc");
		}

	    [Test]
	    public void GetSlug_WithNull_ReturnsNull()
	    {
            var result = SlugGenerator.GetSlug(null);

            Assert.IsNull(result);
	    }
	}
}