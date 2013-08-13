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
			$this->currentUser = new User();
			$this->homegame = new Homegame();
			$this->player = new Player();
			$this->user = new User();
			$this->cashgames = array();
			$this->isManager = false;
			$this->hasPlayed = false;
			$avatarService = TestHelper::getFake(ClassNames::$AvatarService);
			$this->avatarModelBuilder = new AvatarModelBuilder($avatarService);
		}

		function test_DisplayName_IsSet(){
			$this->player->setDisplayName('a');

			$sut = $this->getSut();

			$this->assertIdentical('a', $sut->displayName);
		}

		function test_DeleteUrl_IsSet(){
			$sut = $this->getSut();

			$this->assertIsA($sut->deleteUrl, 'app\Urls\PlayerDeleteUrlModel');
		}

		function test_DeleteEnabled_NotManager_IsFalse(){
			$sut = $this->getSut();

			$this->assertFalse($sut->deleteEnabled);
		}

		function test_DeleteEnabled_IsManagerAndPlayerHasNotPlayed_IsTrue(){
			$this->isManager = true;

			$sut = $this->getSut();

			$this->assertTrue($sut->deleteEnabled);
		}

		function test_DeleteEnabled_IsManagerAndPlayerHasPlayed_IsFalse(){
			$this->isManager = true;
			$this->hasPlayed = true;

			$sut = $this->getSut();

			$this->assertFalse($sut->deleteEnabled);
		}

		function test_ShowUserInfo_WithUser_IsTrue(){
			$sut = $this->getSut();

			$this->assertTrue($sut->showUserInfo);
		}

		function test_UserUrl_WithUser_IsCorrectType(){
			$sut = $this->getSut();

			$this->assertIsA($sut->userUrl, 'app\Urls\UserDetailsUrlModel');
		}

		function test_UserEmail_WithUser_IsSet(){
			$this->user->setEmail('valid@email.com');

			$sut = $this->getSut();

			$this->assertIdentical('valid@email.com', $sut->userEmail);
		}

		function test_AvatarModel_IsCorrectType(){
			$sut = $this->getSut();

			$this->assertIsA($sut->avatarModel, 'app\User\Avatar\AvatarModel');
		}

		function test_PlayerFactsModel_IsCorrectType(){
			$sut = $this->getSut();

			$this->assertIsA($sut->playerFactsModel, 'app\Player\Facts\PlayerFactsModel');
		}

		function test_PlayerAchievementsModel_IsCorrectType(){
			$sut = $this->getSut();

			$this->assertIsA($sut->playerAchievementsModel, 'app\Player\Achievements\PlayerAchievementsModel');
		}

		function test_ShowInvitation_WithUser_IsFalse(){
			$sut = $this->getSut();

			$this->assertFalse($sut->showInvitation);
		}

		function test_ShowUserInfo_WithoutUser_IsFalse(){
			$this->user = null;

			$sut = $this->getSut();

			$this->assertFalse($sut->showUserInfo);
		}

		function test_ShowInvitation_WithoutUser_IsTrue(){
			$this->user = null;

			$sut = $this->getSut();

			$this->assertTrue($sut->showInvitation);
		}

		function test_InvitationUrl_WithoutUser_IsCorrectType(){
			$this->user = null;

			$sut = $this->getSut();

			$this->assertIsA($sut->invitationUrl, 'app\Urls\PlayerInviteUrlModel');
		}

		private function getSut(){
			return new PlayerDetailsModel($this->currentUser, $this->homegame, $this->player, $this->user, $this->cashgames, $this->isManager, $this->hasPlayed, $this->avatarModelBuilder);
		}

	}

}