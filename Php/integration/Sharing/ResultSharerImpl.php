<?php
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
			$this->sharingStorage = $sharingStorage;
			$this->userStorage = $userStorage;
			$this->socialServiceFactory = $socialServiceFactory;
		}

		public function shareResult(Cashgame $cashgame){
			$results = $cashgame->getResults();
			foreach($results as $result){
				$this->shareSingleResult($result);
			}
		}

		public function shareSingleResult(CashgameResult $result){
			$user = $this->getUser($result);
			$services = $this->sharingStorage->getServices($user);
			foreach($services as $serviceName){
				$this->shareToService($serviceName, $user, $result);
			}
		}

		public function shareToService($serviceName, User $user, CashgameResult $result){
			$service = $this->socialServiceFactory->makeSocialService($serviceName);
			$service->shareResult($user, $result->getWinnings());
		}

		private function getUser(CashgameResult $result){
			return $this->userStorage->getUserByName($result->getPlayer()->getUserName());
		}

	}

}