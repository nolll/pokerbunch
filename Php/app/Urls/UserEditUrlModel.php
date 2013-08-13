namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\UserUrlModel;
	use Domain\Classes\User;

	class UserEditUrlModel extends UserUrlModel{

		public function __construct(User $user){
			parent::__construct(RouteFormats::userEdit, $user);
		}

	}

}