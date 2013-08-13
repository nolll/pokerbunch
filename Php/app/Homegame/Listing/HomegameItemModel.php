namespace app\Homegame\Listing{

	use entities\Homegame;
	use app\Urls\HomegameDetailsUrlModel;

	class HomegameItemModel{

		public $name;
		public $urlModel;

		public function __construct(Homegame $homegame){
			name = $homegame.getDisplayName();
			urlModel = new HomegameDetailsUrlModel($homegame);
		}

	}

}