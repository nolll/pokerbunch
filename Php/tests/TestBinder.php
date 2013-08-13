<?php
namespace tests{

	use Sharbat\Inject\AbstractModule;

	class TestBinder extends AbstractModule {

		/** @var ClassBinding[] */
		private $fakeBindings;

		public function __construct(){
			$this->fakeBindings = array();
		}

		public function addClassBinding(ClassBinding $classBinding){
			$this->fakeBindings[] = $classBinding;
		}

		public function configure() {
			foreach($this->fakeBindings as $classBinding){
				$this->bind($classBinding->getInterfaceName())
					->toInstance(TestHelper::getFakeFromClassBinding($classBinding))
					->inSingleton();
			}
		}

	}

}