using Core.Services;
using NUnit.Framework;

namespace Core.Tests.Services{

	public class EncryptionServiceTests {

        [Test]
		public void Encrypt_ReturnsSha1EncryptedString(){
			var sut = GetSut();
            var result = sut.Encrypt("string", "salt");

			Assert.AreEqual(40, result.Length);
		}

	    private EncryptionService GetSut()
	    {
	        return new EncryptionService();
	    }
	}

}