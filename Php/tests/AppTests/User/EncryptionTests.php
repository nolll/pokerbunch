namespace tests\User{

	use app\User\EncryptionImpl;
	use tests\UnitTestCase;

	class EncryptionTests extends UnitTestCase {

		function test_Encrypt_ReturnsSha1EncryptedString(){
			$encryption = new EncryptionImpl();

			$encryptedString = $encryption.encrypt("string", "salt");

			assertIdentical(40, strlen($encryptedString));
		}

	}

}