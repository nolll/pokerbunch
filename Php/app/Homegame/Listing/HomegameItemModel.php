namespace app\Homegame\Listing{

	use entities\Homegame;
	use app\Urls\HomegameDetailsUrlModel;

	class HomegameItemModel{

		public $name;
		public $urlModel;

		public function __construct(Homegame $homegame){
			$this->name = $homegame->getDisplayName();
			$this->urlModel = new HomegameDetailsUrlModel($homegame);
		}

	}

}