<?php
namespace app{

	use Domain\Interfaces\HomegameRepository;
	use core\DateTimeFactory;
	use Mishiin\ParamParser;
	use Sharbat\Inject\Injector;

	class AppParamParser extends ParamParser {

		/*
		private $homegameRepository;

		public function __construct(HomegameRepository $homegameRepository){
			$this->homegameRepository = $homegameRepository;
		}
		*/

		private $injector;

		public function __construct(Injector $injector){
			$this->injector = $injector;
		}

		protected function parseParam($param, $paramType){
			if($paramType == null){
				return $param;
			}
			if($paramType == 'DateTime'){
				return DateTimeFactory::create($param);
			}
			if($paramType == 'entities\Homegame'){
				return $this->getHomegameRepository()->getByName($param);
			}
			return $param;
		}

		/**
		 * @return HomegameRepository
		 */
		private function getHomegameRepository(){
			return $this->injector->getInstance('Domain\Repositories\HomegameRepository');
		}

	}

}