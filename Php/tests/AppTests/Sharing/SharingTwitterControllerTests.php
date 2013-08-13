namespace tests\AppTests\Sharing{

	use app\Sharing\Twitter\SharingTwitterController;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class SharingTwitterControllerTests extends UnitTestCase {

		/** @var SharingTwitterController */
		private $sut;
		private $userContext;
		private $sharingStorage;
		private $response;
		private $twitterService;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			sharingStorage = TestHelper::getFake(ClassNames::$SharingStorage);
			$twitterStorage = TestHelper::getFake(ClassNames::$TwitterStorage);
			twitterService = TestHelper::getFake(ClassNames::$TwitterService);
			response = TestHelper::getFake(ClassNames::$Response);
			$request = TestHelper::getFake(ClassNames::$Request);
			sut = new SharingTwitterController(userContext, sharingStorage, $twitterStorage, twitterService, response, $request);
		}

		function test_ActionTwitter_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireUser');
			expectException();

			sut.action_twitter("homegame1");
		}

		function test_ActionTwitterstart_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireUser');
			expectException();

			sut.action_twitterstart();
		}

		function test_ActionTwitterstart_CallsSaveRequestTokenToSession(){
			TestHelper::setupUser(userContext);
			$notNullObject = "notNull";
			twitterService.returns("getRequestToken", $notNullObject);
			twitterService.expectOnce("saveRequestTokenToSession");

			sut.action_twitterstart();
		}

		function test_ActionTwitterstart_CallsGetAuthUrl(){
			TestHelper::setupUser(userContext);
			$notNullObject = "notNull";
			twitterService.returns("getRequestToken", $notNullObject);
			twitterService.expectOnce("getAuthUrl");

			sut.action_twitterstart();
		}

		function test_ActionTwitterstart_RedirectsToAuthUrl(){
			TestHelper::setupUser(userContext);
			$notNullObject = "notNull";
			twitterService.returns("getRequestToken", $notNullObject);
			twitterService.returns("getAuthUrl", "authUrl");
			response.expectOnce("redirect", array("authUrl"));

			sut.action_twitterstart();
		}

		function test_ActionTwitterstop_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireUser');
			expectException();

			sut.action_twitterstop();
		}

		function test_ActionTwitterstop_CallsRemoveSharing(){
			$user = TestHelper::setupUser(userContext);

			sharingStorage.expectOnce("removeSharing", array($user, "twitter"));

			sut.action_twitterstop();
		}

		function test_ActionTwitterstop_RedirectsToTwitterSettings(){
			TestHelper::setupUser(userContext);

			$urlModel = sut.action_twitterstop();

			assertIsA($urlModel, 'app\Urls\TwitterSettingsUrlModel');
		}

		function test_ActionTwittercallback_CallsAddSharing(){
			$user = TestHelper::setupUser(userContext);

			twitterService.returns("verifyTempTokenOrClearSession", true);
			$notNullObject = "notNull";
			twitterService.returns("getAccessToken", $notNullObject);
			sharingStorage.expectOnce("addSharing", array($user, "twitter"));

			sut.action_twittercallback();
		}

		function test_ActionTwittercallback_RedirectsToTwitterSettings(){
			TestHelper::setupUser(userContext);

			$urlModel = sut.action_twittercallback();

			assertIsA($urlModel, 'app\Urls\TwitterSettingsUrlModel');
		}

	}

}