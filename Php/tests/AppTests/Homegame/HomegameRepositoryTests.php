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
			homegameStorage = registerFake(ClassNames::$HomegameStorage);
			homegameRepository = new HomegameRepositoryImpl(homegameStorage);
		}

		function test_GetByName_NoHomegameFound_ReturnsNull(){
			homegameStorage.returns('getHomegameByName', null);

			$homegame = homegameRepository.getByName('anyname');

			assertNull($homegame);
		}

		function test_GetByName_HomegameFound_ReturnsHomegame(){
			$homegame = new RawHomegame();
			$homegame.setSlug('a');
			$homegame.setTimezoneName('UTC');

			homegameStorage.returns('getRawHomegameByName', $homegame);

			$homegame = homegameRepository.getByName('a');

			assertEqual($homegame.getSlug(), 'a');
		}

	}

}