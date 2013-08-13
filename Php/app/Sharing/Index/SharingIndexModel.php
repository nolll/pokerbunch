namespace app\Sharing\Index{

	use core\PageModel;
	use app\Urls\TwitterSettingsUrlModel;
	use Domain\Classes\User;

	class SharingIndexModel extends PageModel {

		public $isSharingToTwitter;
		public $shareToTwitterSettingsUrl;

		public function __construct(User $user, $isSharing){
			parent::__construct($user);
			isSharingToTwitter = $isSharing;
			shareToTwitterSettingsUrl = new TwitterSettingsUrlModel();
		}

	}

}