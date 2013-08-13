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
			messageSenderFactory = registerFake(ClassNames::$MessageSenderFactory);
			settings = registerFake(ClassNames::$Settings);
			passwordSender = getInstance('app\User\ForgotPassword\PasswordSenderImpl');
		}

		function test_GetSubject_ReturnsSubjectThatIncludesHomegameDisplayName(){
			$subject = passwordSender.getSubject();

			assertTrue(strlen($subject) > 0);
		}

		function test_GetBody_ReturnsBodyThatIncludesLoginUrl(){
			$loginUrl = new AuthLoginUrlModel();
			$body = passwordSender.getBody('anypassword');

			assertTrue(strpos($body, $loginUrl.url));
		}

		function test_GetBody_ReturnsBodyThatIncludesPassword(){
			$password = 'generated-password';
			$body = passwordSender.getBody($password);

			assertTrue(strpos($body, $password));
		}

		function test_Send_CallsSendOnMessageSender(){
			$user = new User();
			$messageSender = TestHelper::getFake('integration\Message\MessageSender');
			messageSenderFactory.returns("getMessageSender", $messageSender);
			$messageSender.expectOnce("send");

			passwordSender.send($user, "anypassword");
		}

	}

}