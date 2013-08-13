namespace tests\AppTests\Player{

	use entities\Homegame;
	use entities\Player;
	use tests\UnitTestCase;
	use Domain\Classes\User;
	use core\ClassNames;
	use app\User\Avatar\AvatarModelBuilder;
	use app\Player\Details\PlayerDetailsModel;
	use tests\TestHelper;

	class PlayerDetailsModelTests extends UnitTestCase {

		private $currentUser;
		private $homegame;
		/** @var Player */
		private $player;
		/** @var User */
		private $user;
		private $cashgames;
		private $isManager;
		private $hasPlayed;
		private $avatarModelBuilder;

		function setUp(){
			parent::setUp();
			currentUser = new User();
			homegame = new Homegame();
			player = new Player();
			user = new User();
			cashgames = array();
			isManager = false;
			hasPlayed = false;
			$avatarService = TestHelper::getFake(ClassNames::$AvatarService);
			avatarModelBuilder = new AvatarModelBuilder($avatarService);
		}

		function test_DisplayName_IsSet(){
			player.setDisplayName('a');

			$sut = getSut();

			assertIdentical('a', $sut.displayName);
		}

		function test_DeleteUrl_IsSet(){
			$sut = getSut();

			assertIsA($sut.deleteUrl, 'app\Urls\PlayerDeleteUrlModel');
		}

		function test_DeleteEnabled_NotManager_IsFalse(){
			$sut = getSut();

			assertFalse($sut.deleteEnabled);
		}

		function test_DeleteEnabled_IsManagerAndPlayerHasNotPlayed_IsTrue(){
			isManager = true;

			$sut = getSut();

			assertTrue($sut.deleteEnabled);
		}

		function test_DeleteEnabled_IsManagerAndPlayerHasPlayed_IsFalse(){
			isManager = true;
			hasPlayed = true;

			$sut = getSut();

			assertFalse($sut.deleteEnabled);
		}

		function test_ShowUserInfo_WithUser_IsTrue(){
			$sut = getSut();

			assertTrue($sut.showUserInfo);
		}

		function test_UserUrl_WithUser_IsCorrectType(){
			$sut = getSut();

			assertIsA($sut.userUrl, 'app\Urls\UserDetailsUrlModel');
		}

		function test_UserEmail_WithUser_IsSet(){
			user.setEmail('valid@email.com');

			$sut = getSut();

			assertIdentical('valid@email.com', $sut.userEmail);
		}

		function test_AvatarModel_IsCorrectType(){
			$sut = getSut();

			assertIsA($sut.avatarModel, 'app\User\Avatar\AvatarModel');
		}

		function test_PlayerFactsModel_IsCorrectType(){
			$sut = getSut();

			assertIsA($sut.playerFactsModel, 'app\Player\Facts\PlayerFactsModel');
		}

		function test_PlayerAchievementsModel_IsCorrectType(){
			$sut = getSut();

			assertIsA($sut.playerAchievementsModel, 'app\Player\Achievements\PlayerAchievementsModel');
		}

		function test_ShowInvitation_WithUser_IsFalse(){
			$sut = getSut();

			assertFalse($sut.showInvitation);
		}

		function test_ShowUserInfo_WithoutUser_IsFalse(){
			user = null;

			$sut = getSut();

			assertFalse($sut.showUserInfo);
		}

		function test_ShowInvitation_WithoutUser_IsTrue(){
			user = null;

			$sut = getSut();

			assertTrue($sut.showInvitation);
		}

		function test_InvitationUrl_WithoutUser_IsCorrectType(){
			user = null;

			$sut = getSut();

			assertIsA($sut.invitationUrl, 'app\Urls\PlayerInviteUrlModel');
		}

		private function getSut(){
			return new PlayerDetailsModel(currentUser, homegame, player, user, cashgames, isManager, hasPlayed, avatarModelBuilder);
		}

	}

}