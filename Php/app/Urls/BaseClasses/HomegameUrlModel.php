namespace app\Urls\BaseClasses{

	use app\UrlFormatter;
	use entities\Homegame;

	class HomegameUrlModel extends UrlModel{

		public function __construct($format, Homegame $homegame){
			$url = UrlFormatter::formatHomegame($format, $homegame);
			parent::__construct($url);
		}

	}

}