namespace tests\CoreTests{

	use core\ClassNames;
	use integration\Avatar\GravatarService;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class GravatarTests extends UnitTestCase {

		private $settings;

		function setUp(){
			settings = TestHelper::getFake(ClassNames::$Settings);
		}

		function testSmallGravatarUrl(){
			$gravatarEmail = "henriks@gmail.com";
			settings.returns('getSiteUrl', 'site-url');
			$expectedUrl = "http://www.gravatar.com/avatar/24a827c683a7646cde86696b418b20b4?s=40&d=site-url/core/ui/img/pix.gif";
			$gravatarService = getGravatarService();

			$gravatarUrl = $gravatarService.getSmallAvatarUrl($gravatarEmail);
			assertIdentical($expectedUrl, $gravatarUrl);
		}

		function testLargeGravatarUrl(){
			$gravatarEmail = "henriks@gmail.com";
			settings.returns('getSiteUrl', 'site-url');
			$expectedUrl = "http://www.gravatar.com/avatar/24a827c683a7646cde86696b418b20b4?s=100&d=site-url/core/ui/img/pix.gif";
			$gravatarService = getGravatarService();

			$gravatarUrl = $gravatarService.getLargeAvatarUrl($gravatarEmail);
			assertIdentical($expectedUrl, $gravatarUrl);
		}

		private function getGravatarService(){
			return new GravatarService(settings);
		}

	}

}