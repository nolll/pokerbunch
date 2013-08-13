namespace core\Navigation{

	class NavigationModel {

		public $heading;
		public $headingLink;
		public $headingIsLinked;
		public $nodes;
		public $cssClass;
		public $dataRequire;

		public function __construct($heading = null, $nodes = null, $cssClass = null){
			nodes = array();
			if($heading != null){
				heading = $heading;
			}
			if($nodes != null){
				nodes = $nodes;
			}
			if($cssClass != null){
				cssClass = $cssClass;
			}
		}

		protected function addNode(NavigationNode $node){
			nodes[] = $node;
		}

	}

}