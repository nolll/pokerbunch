<?php
namespace integration\Sharing{

	class SocialServiceFactoryImpl implements SocialServiceFactory{

		private $twitterService;

		public function __construct(TwitterService $twitterService){
			$this->twitterService = $twitterService;
		}

		public function makeSocialService($provider){
			if($provider == SocialServiceProvider::twitter){
				return $this->twitterService;
			}
			return null;
		}

	}

}