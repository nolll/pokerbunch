<?php
namespace tests\User{

	use app\User\SaltGenerator;
	use tests\UnitTestCase;

	class SaltGeneratorTests extends UnitTestCase {

		function test_CreateSalt_Returns10CharSalt(){
			$saltLength = 10;
			$generator = new SaltGenerator();

			$salt = $generator->createSalt();

			$this->assertIdentical($saltLength, strlen($salt));
		}

	}

}