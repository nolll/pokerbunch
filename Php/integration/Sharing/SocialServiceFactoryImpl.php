namespace integration\Sharing{

	class SocialServiceFactoryImpl implements SocialServiceFactory{

		private $twitterService;

		public function __construct(TwitterService $twitterService){
			twitterService = $twitterService;
		}

		public function makeSocialService($provider){
			if($provider == SocialServiceProvider::twitter){
				return twitterService;
			}
			return null;
		}

	}

}