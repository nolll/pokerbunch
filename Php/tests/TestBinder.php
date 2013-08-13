namespace tests{

	use Sharbat\Inject\AbstractModule;

	class TestBinder extends AbstractModule {

		/** @var ClassBinding[] */
		private $fakeBindings;

		public function __construct(){
			fakeBindings = array();
		}

		public function addClassBinding(ClassBinding $classBinding){
			fakeBindings[] = $classBinding;
		}

		public function configure() {
			foreach(fakeBindings as $classBinding){
				bind($classBinding.getInterfaceName())
					.toInstance(TestHelper::getFakeFromClassBinding($classBinding))
					.inSingleton();
			}
		}

	}

}