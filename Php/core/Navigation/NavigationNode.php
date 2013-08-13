namespace core\Navigation{

	use app\Urls\BaseClasses\UrlModel;

	class NavigationNode{

		public $name;
        public $urlModel;
		public $selected;

		public function __construct($name, UrlModel $urlModel, $selected = false){
			$this->name = $name;
			$this->urlModel = $urlModel;
			$this->selected = $selected;
		}

		public function getName(){
			return $this->name;
		}

		public function getUrl(){
			return $this->urlModel;
		}

		public function isSelected(){
			return $this->selected;
		}

	}

}