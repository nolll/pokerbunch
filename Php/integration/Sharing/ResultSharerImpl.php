namespace integration\Sharing{

	use entities\Cashgame;
	use Infrastructure\Data\Interfaces\SharingStorage;
	use Infrastructure\Data\Interfaces\UserStorage;
	use entities\CashgameResult;
	use Domain\Classes\User;

	class ResultSharerImpl implements ResultSharer{

		private $sharingStorage;
		private $userStorage;
		private $socialServiceFactory;

		public function __construct(SharingStorage $sharingStorage,
									UserStorage $userStorage,
									SocialServiceFactory $socialServiceFactory){
			sharingStorage = $sharingStorage;
			userStorage = $userStorage;
			socialServiceFactory = $socialServiceFactory;
		}

		public function shareResult(Cashgame $cashgame){
			$results = $cashgame.getResults();
			foreach($results as $result){
				shareSingleResult($result);
			}
		}

		public function shareSingleResult(CashgameResult $result){
			$user = getUser($result);
			$services = sharingStorage.getServices($user);
			foreach($services as $serviceName){
				shareToService($serviceName, $user, $result);
			}
		}

		public function shareToService($serviceName, User $user, CashgameResult $result){
			$service = socialServiceFactory.makeSocialService($serviceName);
			$service.shareResult($user, $result.getWinnings());
		}

		private function getUser(CashgameResult $result){
			return userStorage.getUserByName($result.getPlayer().getUserName());
		}

	}

}