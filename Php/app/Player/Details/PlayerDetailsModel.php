namespace app\Player\Details{

	use core\HomegamePageModel;
	use app\User\Avatar\AvatarModelBuilder;
	use app\Player\Achievements\PlayerAchievementsModel;
	use app\Player\Facts\PlayerFactsModel;
	use app\Urls\PlayerInviteUrlModel;
	use app\Urls\UserDetailsUrlModel;
	use app\Urls\PlayerDeleteUrlModel;
	use entities\Cashgame;
	use entities\Homegame;
	use entities\Player;
	use Domain\Classes\User;

	class PlayerDetailsModel extends HomegamePageModel {

		public $showUserInfo;
		public $showInvitation;
		public $displayName;
		public $deleteEnabled;
		public $deleteUrl;
		public $userUrl;
		public $invitationUrl;
		public $userEmail;
		public $avatarUrl;
		public $avatarModel;
		public $playerFactsModel;
		public $playerAchievementsModel;

		public function __construct(User $currentUser,
									Homegame $homegame,
									Player $player,
									User $user = null,
									array $cashgames,
									$isManager,
									$hasPlayed,
									AvatarModelBuilder $avatarModelBuilder,
									Cashgame $runningGame = null){
			parent::__construct($currentUser, $homegame, $runningGame);
			displayName = $player.getDisplayName();
			deleteUrl = new PlayerDeleteUrlModel($homegame, $player);
			deleteEnabled = $isManager && !$hasPlayed;
			$hasUser = $user != null;
			showUserInfo = $hasUser;
			showInvitation = !$hasUser;
			if($hasUser){
				userUrl = new UserDetailsUrlModel($user);
				userEmail = $user.getEmail();
				avatarModel = $avatarModelBuilder.build($user.getEmail());
			} else {
				invitationUrl = new PlayerInviteUrlModel($homegame, $player);
			}
			playerFactsModel = new PlayerFactsModel($homegame, $cashgames, $player);
			playerAchievementsModel = new PlayerAchievementsModel($player, $cashgames);
		}

	}

}