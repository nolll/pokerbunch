namespace core\Navigation{

	class NavigationModel {

		public $heading;
		public $headingLink;
		public $headingIsLinked;
		public $nodes;
		public $cssClass;
		public $dataRequire;

		public function __construct($heading = null, $nodes = null, $cssClass = null){
			$this->nodes = array();
			if($heading != null){
				$this->heading = $heading;
			}
			if($nodes != null){
				$this->nodes = $nodes;
			}
			if($cssClass != null){
				$this->cssClass = $cssClass;
			}
		}

		protected function addNode(NavigationNode $node){
			$this->nodes[] = $node;
		}

	}

}