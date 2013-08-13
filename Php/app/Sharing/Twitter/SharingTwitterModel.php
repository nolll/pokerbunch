namespace app\Sharing\Twitter{

	use core\PageModel;
	use app\Urls\TwitterStartShareUrlModel;
	use app\Urls\TwitterStopShareUrlModel;
	use integration\Sharing\TwitterCredentials;
	use Domain\Classes\User;

	class SharingTwitterModel extends PageModel {

		public $twitterName;
		public $isSharing;
		public $postUrl;

		public function __construct(User $user, $isSharing, TwitterCredentials $credentials = null){
			parent::__construct($user);
			if($isSharing){
				if($credentials != null){
					twitterName = $credentials.twitterName;
				}
			}
			isSharing = $isSharing;
			if($isSharing){
				postUrl = new TwitterStopShareUrlModel();
			} else {
				postUrl = new TwitterStartShareUrlModel();
			}
		}

	}

}