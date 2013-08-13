namespace app\Urls\BaseClasses{

	use Domain\Classes\User;
	use app\UrlFormatter;

	class UserUrlModel extends UrlModel{

		public function __construct($format, User $user){
			$url = UrlFormatter::formatUser($format, $user);
			parent::__construct($url);
		}

	}

}