namespace tests\AppTests\Homegame{

	use Domain\Services\HomegameRepositoryImpl;
	use Infrastructure\Data\Classes\RawHomegame;
	use tests\SharbatUnitTestCase;
	use core\ClassNames;
	use tests\TestHelper;
	use entities\Homegame;

	class HomegameRepositoryTests extends SharbatUnitTestCase {

		/** @var HomegameRepositoryImpl */
		private $homegameRepository;
		private $homegameStorage;

		function setUp(){
			parent::setUp();
			$this->homegameStorage = $this->registerFake(ClassNames::$HomegameStorage);
			$this->homegameRepository = new HomegameRepositoryImpl($this->homegameStorage);
		}

		function test_GetByName_NoHomegameFound_ReturnsNull(){
			$this->homegameStorage->returns('getHomegameByName', null);

			$homegame = $this->homegameRepository->getByName('anyname');

			$this->assertNull($homegame);
		}

		function test_GetByName_HomegameFound_ReturnsHomegame(){
			$homegame = new RawHomegame();
			$homegame->setSlug('a');
			$homegame->setTimezoneName('UTC');

			$this->homegameStorage->returns('getRawHomegameByName', $homegame);

			$homegame = $this->homegameRepository->getByName('a');

			$this->assertEqual($homegame->getSlug(), 'a');
		}

	}

}