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
			binder = new TestBinder();
			bind();
			injector = Sharbat::createInjector(binder);
		}

		public function bind(){}

		protected function bindFakeClass($interfaceName, $mockClassName = null){
			$classBinding = new ClassBinding($interfaceName, $mockClassName);
			binder.addClassBinding($classBinding);
		}

		protected function registerFake($interfaceName){
			$classBinding = new ClassBinding($interfaceName);
			$instance = TestHelper::getFakeFromClassBinding($classBinding);
			return registerInstance($interfaceName, $instance);
		}

		protected function registerInstance($interfaceName, $instance){
			binder.bind($interfaceName)
				.toInstance($instance)
				.inSingleton();
			return injector.getInstance($interfaceName);
		}

		protected function getInstance($className){
			return injector.getInstance($className);
		}

	}

}