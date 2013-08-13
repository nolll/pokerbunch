namespace core{

	use Mishiin\DefaultControllerDefinition;
	use Mishiin\Request;

	class HomegameControllerDefinition extends DefaultControllerDefinition {

		private $homegameSlug;

		protected function extractControllerData(){
			$this->homegameSlug = $this->extractHomegameSlug();
			parent::extractControllerData();
			$this->insertHomegameParam();
		}

		protected function extractHomegameSlug(){
			$slug = $this->urlSegmenter->getNextUrlSegment();
			if($slug == '-'){
				$slug = null;
			}
			return $slug;
		}

		private function insertHomegameParam(){
			if($this->homegameSlug != null){
				array_unshift($this->params, $this->homegameSlug);
			}
		}

		protected function getDefaultName(){
			return $this->homegameSlug != null ? 'game' : $this->defaultName;
		}

		protected function getDefaultAction(){
			return $this->homegameSlug != null && $this->name == 'game' ? 'details' : $this->defaultAction;
		}

	}

}