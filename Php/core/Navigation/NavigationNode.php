namespace core\Navigation{

	use app\Urls\BaseClasses\UrlModel;

	class NavigationNode{

		public $name;
        public $urlModel;
		public $selected;

		public function __construct($name, UrlModel $urlModel, $selected = false){
			name = $name;
			urlModel = $urlModel;
			selected = $selected;
		}

		public function getName(){
			return name;
		}

		public function getUrl(){
			return urlModel;
		}

		public function isSelected(){
			return selected;
		}

	}

}