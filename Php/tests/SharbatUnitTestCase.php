<?php
namespace tests{

	use Sharbat\Sharbat;
	use Mishiin\Request;
	use Sharbat\Inject\Injector;

	abstract class SharbatUnitTestCase extends UnitTestCase {

		/** @var TestBinder */
		public $binder;

		/** @var Injector */
		private $injector;

		public function setUp(){
			$this->binder = new TestBinder();
			$this->bind();
			$this->injector = Sharbat::createInjector($this->binder);
		}

		public function bind(){}

		protected function bindFakeClass($interfaceName, $mockClassName = null){
			$classBinding = new ClassBinding($interfaceName, $mockClassName);
			$this->binder->addClassBinding($classBinding);
		}

		protected function registerFake($interfaceName){
			$classBinding = new ClassBinding($interfaceName);
			$instance = TestHelper::getFakeFromClassBinding($classBinding);
			return $this->registerInstance($interfaceName, $instance);
		}

		protected function registerInstance($interfaceName, $instance){
			$this->binder->bind($interfaceName)
				->toInstance($instance)
				->inSingleton();
			return $this->injector->getInstance($interfaceName);
		}

		protected function getInstance($className){
			return $this->injector->getInstance($className);
		}

	}

}