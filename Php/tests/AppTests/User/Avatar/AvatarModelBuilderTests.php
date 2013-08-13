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
			avatarService = registerFake(ClassNames::$AvatarService);
			modelBuilder = new AvatarModelBuilder(avatarService);
		}

		function test_Build_WithEmailAndLargeSize_LargeAvatarUrlIsSetAndAvatarEnabledIsTrue(){
			$email = "anyemail";
			avatarService.returns("getLargeAvatarUrl", "avatar-url");

			$model = modelBuilder.build($email, AvatarSize::large);

			assertTrue($model.avatarEnabled);
			assertIdentical("avatar-url", $model.avatarUrl);
		}

		function test_Build_WithEmailAndSmallSize_LargeAvatarUrlIsSetAndAvatarEnabledIsTrue(){
			$email = "anyemail";
			avatarService.returns("getSmallAvatarUrl", "avatar-url");

			$model = modelBuilder.build($email, AvatarSize::small);

			assertTrue($model.avatarEnabled);
			assertIdentical("avatar-url", $model.avatarUrl);
		}

		function test_Build_NoEmail_AvatarEnabledIsFalse(){
			$email = null;

			$model = modelBuilder.build($email, AvatarSize::large);

			assertFalse($model.avatarEnabled);
		}

	}

}