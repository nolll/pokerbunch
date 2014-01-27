using Application.Services;
using NUnit.Framework;

namespace Tests.Application.Services{

	public class EncryptionServiceTests {

        [Test]
		public void Encrypt_ReturnsSha1EncryptedString()
        {
            const string expected = "da23614e02469a0d7c7bd1bdab5c9c474b1904dc";

			var sut = GetSut();
            var result = sut.Encrypt("a", "b");

			Assert.AreEqual(expected, result);
		}

	    [Test]
	    public void GetMd5Hash_ReturnsMd5Hash()
	    {
            const string expected = "0cc175b9c0f1b6a831c399e269772661";

	        var sut = GetSut();
	        var result = sut.GetMd5Hash("a");

            Assert.AreEqual(expected, result);
	    }


	    private EncryptionService GetSut()
	    {
	        return new EncryptionService();
	    }
	}

}