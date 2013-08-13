namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\UrlModel;

	class UserListingUrlModel extends UrlModel{

		public function __construct(){
			parent::__construct(RouteFormats::userListing);
		}

	}

}