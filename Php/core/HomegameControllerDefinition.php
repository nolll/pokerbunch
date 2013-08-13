namespace core{

	use Mishiin\DefaultControllerDefinition;
	use Mishiin\Request;

	class HomegameControllerDefinition extends DefaultControllerDefinition {

		private $homegameSlug;

		protected function extractControllerData(){
			homegameSlug = extractHomegameSlug();
			parent::extractControllerData();
			insertHomegameParam();
		}

		protected function extractHomegameSlug(){
			$slug = urlSegmenter.getNextUrlSegment();
			if($slug == '-'){
				$slug = null;
			}
			return $slug;
		}

		private function insertHomegameParam(){
			if(homegameSlug != null){
				array_unshift(params, homegameSlug);
			}
		}

		protected function getDefaultName(){
			return homegameSlug != null ? 'game' : defaultName;
		}

		protected function getDefaultAction(){
			return homegameSlug != null && name == 'game' ? 'details' : defaultAction;
		}

	}

}