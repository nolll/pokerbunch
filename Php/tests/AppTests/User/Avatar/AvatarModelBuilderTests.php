<?php
namespace tests\AppTests\User\Avatar{

	use tests\SharbatUnitTestCase;
	use core\ClassNames;
	use app\User\Avatar\AvatarModelBuilder;
	use integration\Avatar\AvatarSize;
	use tests\TestHelper;

	class AvatarModelBuilderTests extends SharbatUnitTestCase {

		/** @var AvatarModelBuilder */
		private $modelBuilder;
		private $avatarService;

		function setUp(){
			parent::setUp();
			$this->avatarService = $this->registerFake(ClassNames::$AvatarService);
			$this->modelBuilder = new AvatarModelBuilder($this->avatarService);
		}

		function test_Build_WithEmailAndLargeSize_LargeAvatarUrlIsSetAndAvatarEnabledIsTrue(){
			$email = "anyemail";
			$this->avatarService->returns("getLargeAvatarUrl", "avatar-url");

			$model = $this->modelBuilder->build($email, AvatarSize::large);

			$this->assertTrue($model->avatarEnabled);
			$this->assertIdentical("avatar-url", $model->avatarUrl);
		}

		function test_Build_WithEmailAndSmallSize_LargeAvatarUrlIsSetAndAvatarEnabledIsTrue(){
			$email = "anyemail";
			$this->avatarService->returns("getSmallAvatarUrl", "avatar-url");

			$model = $this->modelBuilder->build($email, AvatarSize::small);

			$this->assertTrue($model->avatarEnabled);
			$this->assertIdentical("avatar-url", $model->avatarUrl);
		}

		function test_Build_NoEmail_AvatarEnabledIsFalse(){
			$email = null;

			$model = $this->modelBuilder->build($email, AvatarSize::large);

			$this->assertFalse($model->avatarEnabled);
		}

	}

}