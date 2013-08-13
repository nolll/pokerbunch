namespace app\Homegame{

	class SlugGeneratorImpl implements SlugGenerator{

		public function __construct(){
		}

		public function getSlug($displayName){
			return generateSlug($displayName);
		}

		private function generateSlug($displayName){
			if($displayName == null){
				return null;
			}
			$slug = removeSpaces($displayName);
			$slug = removeCaps($slug);
			return $slug;
		}

		private function removeSpaces($name){
			return str_replace(' ', '', $name);
		}

		private function removeCaps($name){
			return strtolower($name);
		}

	}

}