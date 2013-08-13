<?php
namespace tests\AppTests\User{

	use app\User\ForgotPassword\PasswordSenderImpl;
	use Domain\Classes\User;
	use tests\SharbatUnitTestCase;
	use app\Urls\AuthLoginUrlModel;
	use core\ClassNames;
	use tests\TestHelper;

	class PasswordSenderTests extends SharbatUnitTestCase {

		/** @var PasswordSenderImpl */
		private $passwordSender;
		private $messageSenderFactory;
		private $settings;

		function setUp(){
			parent::setUp();
			$this->messageSenderFactory = $this->registerFake(ClassNames::$MessageSenderFactory);
			$this->settings = $this->registerFake(ClassNames::$Settings);
			$this->passwordSender = $this->getInstance('app\User\ForgotPassword\PasswordSenderImpl');
		}

		function test_GetSubject_ReturnsSubjectThatIncludesHomegameDisplayName(){
			$subject = $this->passwordSender->getSubject();

			$this->assertTrue(strlen($subject) > 0);
		}

		function test_GetBody_ReturnsBodyThatIncludesLoginUrl(){
			$loginUrl = new AuthLoginUrlModel();
			$body = $this->passwordSender->getBody('anypassword');

			$this->assertTrue(strpos($body, $loginUrl->url));
		}

		function test_GetBody_ReturnsBodyThatIncludesPassword(){
			$password = 'generated-password';
			$body = $this->passwordSender->getBody($password);

			$this->assertTrue(strpos($body, $password));
		}

		function test_Send_CallsSendOnMessageSender(){
			$user = new User();
			$messageSender = TestHelper::getFake('integration\Message\MessageSender');
			$this->messageSenderFactory->returns("getMessageSender", $messageSender);
			$messageSender->expectOnce("send");

			$this->passwordSender->send($user, "anypassword");
		}

	}

}