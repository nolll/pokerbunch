namespace app{

	use Domain\Interfaces\HomegameRepository;
	use core\DateTimeFactory;
	use Mishiin\ParamParser;
	use Sharbat\Inject\Injector;

	class AppParamParser extends ParamParser {

		/*
		private $homegameRepository;

		public function __construct(HomegameRepository $homegameRepository){
			homegameRepository = $homegameRepository;
		}
		*/

		private $injector;

		public function __construct(Injector $injector){
			injector = $injector;
		}

		protected function parseParam($param, $paramType){
			if($paramType == null){
				return $param;
			}
			if($paramType == 'DateTime'){
				return DateTimeFactory::create($param);
			}
			if($paramType == 'entities\Homegame'){
				return getHomegameRepository().getByName($param);
			}
			return $param;
		}

		/**
		 * @return HomegameRepository
		 */
		private function getHomegameRepository(){
			return injector.getInstance('Domain\Repositories\HomegameRepository');
		}

	}

}