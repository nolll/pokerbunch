<?php
namespace tests\AppTests\User{

	use app\User\Add\RegistrationConfirmationSenderImpl;
	use Domain\Classes\User;
	use tests\SharbatUnitTestCase;
	use app\Urls\AuthLoginUrlModel;
	use core\ClassNames;
	use tests\TestHelper;

	class RegistrationConfirmationSenderTests extends SharbatUnitTestCase {

		/** @var RegistrationConfirmationSenderImpl */
		private $registrationConfirmationSender;
		private $messageSenderFactory;
		private $settings;

		function setUp(){
			parent::setUp();
			$this->messageSenderFactory = $this->registerFake(ClassNames::$MessageSenderFactory);
			$this->settings = $this->registerFake(ClassNames::$Settings);
			$this->registrationConfirmationSender = $this->getInstance('app\User\Add\RegistrationConfirmationSenderImpl');
		}

		function test_GetSubject_ReturnsSubjectThatIncludesHomegameDisplayName(){
			$subject = $this->registrationConfirmationSender->getSubject();

			$this->assertTrue(strlen($subject) > 0);
		}

		function test_GetBody_ReturnsBodyThatIncludesLoginUrl(){
			$loginUrl = new AuthLoginUrlModel();
			$body = $this->registrationConfirmationSender->getBody('anypassword');

			$this->assertTrue(strpos($body, $loginUrl->url));
		}

		function test_GetBody_ReturnsBodyThatIncludesPassword(){
			$password = 'generated-password';
			$body = $this->registrationConfirmationSender->getBody($password);

			$this->assertTrue(strpos($body, $password));
		}

		function test_Send_CallsSendOnMessageSender(){
			$user = new User();
			$messageSender = TestHelper::getFake('integration\Message\MessageSender');
			$this->messageSenderFactory->returns("getMessageSender", $messageSender);
			$messageSender->expectOnce("send");

			$this->registrationConfirmationSender->send($user, "anypassword");
		}

	}

}