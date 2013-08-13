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
			messageSenderFactory = registerFake(ClassNames::$MessageSenderFactory);
			settings = registerFake(ClassNames::$Settings);
			registrationConfirmationSender = getInstance('app\User\Add\RegistrationConfirmationSenderImpl');
		}

		function test_GetSubject_ReturnsSubjectThatIncludesHomegameDisplayName(){
			$subject = registrationConfirmationSender.getSubject();

			assertTrue(strlen($subject) > 0);
		}

		function test_GetBody_ReturnsBodyThatIncludesLoginUrl(){
			$loginUrl = new AuthLoginUrlModel();
			$body = registrationConfirmationSender.getBody('anypassword');

			assertTrue(strpos($body, $loginUrl.url));
		}

		function test_GetBody_ReturnsBodyThatIncludesPassword(){
			$password = 'generated-password';
			$body = registrationConfirmationSender.getBody($password);

			assertTrue(strpos($body, $password));
		}

		function test_Send_CallsSendOnMessageSender(){
			$user = new User();
			$messageSender = TestHelper::getFake('integration\Message\MessageSender');
			messageSenderFactory.returns("getMessageSender", $messageSender);
			$messageSender.expectOnce("send");

			registrationConfirmationSender.send($user, "anypassword");
		}

	}

}