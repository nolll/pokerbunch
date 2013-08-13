<?php
namespace tests\User{

	use app\User\PasswordGenerator;
	use tests\UnitTestCase;

	class PasswordGeneratorTests extends UnitTestCase {

		function test_CreatePassword_Returns8CharPassword(){
			$passwordLength = 8;
			$generator = new PasswordGenerator();

			$password = $generator->createPassword();

			$this->assertIdentical($passwordLength, strlen($password));
		}

	}

}