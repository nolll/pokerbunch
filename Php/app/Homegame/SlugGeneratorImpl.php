<?php
namespace app\Homegame{

	class SlugGeneratorImpl implements SlugGenerator{

		public function __construct(){
		}

		public function getSlug($displayName){
			return $this->generateSlug($displayName);
		}

		private function generateSlug($displayName){
			if($displayName == null){
				return null;
			}
			$slug = $this->removeSpaces($displayName);
			$slug = $this->removeCaps($slug);
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