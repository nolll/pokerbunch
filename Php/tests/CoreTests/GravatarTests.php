<?php
namespace tests\CoreTests{

	use core\ClassNames;
	use integration\Avatar\GravatarService;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class GravatarTests extends UnitTestCase {

		private $settings;

		function setUp(){
			$this->settings = TestHelper::getFake(ClassNames::$Settings);
		}

		function testSmallGravatarUrl(){
			$gravatarEmail = "henriks@gmail.com";
			$this->settings->returns('getSiteUrl', 'site-url');
			$expectedUrl = "http://www.gravatar.com/avatar/24a827c683a7646cde86696b418b20b4?s=40&d=site-url/core/ui/img/pix.gif";
			$gravatarService = $this->getGravatarService();

			$gravatarUrl = $gravatarService->getSmallAvatarUrl($gravatarEmail);
			$this->assertIdentical($expectedUrl, $gravatarUrl);
		}

		function testLargeGravatarUrl(){
			$gravatarEmail = "henriks@gmail.com";
			$this->settings->returns('getSiteUrl', 'site-url');
			$expectedUrl = "http://www.gravatar.com/avatar/24a827c683a7646cde86696b418b20b4?s=100&d=site-url/core/ui/img/pix.gif";
			$gravatarService = $this->getGravatarService();

			$gravatarUrl = $gravatarService->getLargeAvatarUrl($gravatarEmail);
			$this->assertIdentical($expectedUrl, $gravatarUrl);
		}

		private function getGravatarService(){
			return new GravatarService($this->settings);
		}

	}

}