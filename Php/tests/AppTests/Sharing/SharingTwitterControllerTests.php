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
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->sharingStorage = TestHelper::getFake(ClassNames::$SharingStorage);
			$twitterStorage = TestHelper::getFake(ClassNames::$TwitterStorage);
			$this->twitterService = TestHelper::getFake(ClassNames::$TwitterService);
			$this->response = TestHelper::getFake(ClassNames::$Response);
			$request = TestHelper::getFake(ClassNames::$Request);
			$this->sut = new SharingTwitterController($this->userContext, $this->sharingStorage, $twitterStorage, $this->twitterService, $this->response, $request);
		}

		function test_ActionTwitter_NotAuthorized_ThrowsException(){
			$this->userContext->throwOn('requireUser');
			$this->expectException();

			$this->sut->action_twitter("homegame1");
		}

		function test_ActionTwitterstart_NotAuthorized_ThrowsException(){
			$this->userContext->throwOn('requireUser');
			$this->expectException();

			$this->sut->action_twitterstart();
		}

		function test_ActionTwitterstart_CallsSaveRequestTokenToSession(){
			TestHelper::setupUser($this->userContext);
			$notNullObject = "notNull";
			$this->twitterService->returns("getRequestToken", $notNullObject);
			$this->twitterService->expectOnce("saveRequestTokenToSession");

			$this->sut->action_twitterstart();
		}

		function test_ActionTwitterstart_CallsGetAuthUrl(){
			TestHelper::setupUser($this->userContext);
			$notNullObject = "notNull";
			$this->twitterService->returns("getRequestToken", $notNullObject);
			$this->twitterService->expectOnce("getAuthUrl");

			$this->sut->action_twitterstart();
		}

		function test_ActionTwitterstart_RedirectsToAuthUrl(){
			TestHelper::setupUser($this->userContext);
			$notNullObject = "notNull";
			$this->twitterService->returns("getRequestToken", $notNullObject);
			$this->twitterService->returns("getAuthUrl", "authUrl");
			$this->response->expectOnce("redirect", array("authUrl"));

			$this->sut->action_twitterstart();
		}

		function test_ActionTwitterstop_NotAuthorized_ThrowsException(){
			$this->userContext->throwOn('requireUser');
			$this->expectException();

			$this->sut->action_twitterstop();
		}

		function test_ActionTwitterstop_CallsRemoveSharing(){
			$user = TestHelper::setupUser($this->userContext);

			$this->sharingStorage->expectOnce("removeSharing", array($user, "twitter"));

			$this->sut->action_twitterstop();
		}

		function test_ActionTwitterstop_RedirectsToTwitterSettings(){
			TestHelper::setupUser($this->userContext);

			$urlModel = $this->sut->action_twitterstop();

			$this->assertIsA($urlModel, 'app\Urls\TwitterSettingsUrlModel');
		}

		function test_ActionTwittercallback_CallsAddSharing(){
			$user = TestHelper::setupUser($this->userContext);

			$this->twitterService->returns("verifyTempTokenOrClearSession", true);
			$notNullObject = "notNull";
			$this->twitterService->returns("getAccessToken", $notNullObject);
			$this->sharingStorage->expectOnce("addSharing", array($user, "twitter"));

			$this->sut->action_twittercallback();
		}

		function test_ActionTwittercallback_RedirectsToTwitterSettings(){
			TestHelper::setupUser($this->userContext);

			$urlModel = $this->sut->action_twittercallback();

			$this->assertIsA($urlModel, 'app\Urls\TwitterSettingsUrlModel');
		}

	}

}