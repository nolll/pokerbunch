namespace tests\AppTests\Cashgame\Validators{

	use app\Cashgame\CashgameValidatorFactory;
	use entities\Cashgame;
	use tests\TestHelper;
	use app\Cashgame\Edit\CashgameEditPostModel;
	use tests\UnitTestCase;
	use core\ClassNames;
	use app\Cashgame\CashgameValidatorFactoryImpl;

	class CashgameValidatorTests extends UnitTestCase {

		function test_IsValid_WithValidValues_ReturnsTrue(){
			$postModel = getPostModel();
			$validator = getValidator($postModel);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithEmptyLocation_ReturnsFalse(){
			$postModel = getPostModel();
			$postModel.location = "";
			$validator = getValidator($postModel);

			assertFalse($validator.isValid());
		}

		function test_IsValid_WithValidStartTime_ReturnsTrue(){
			$postModel = getPostModel();
			$validator = getValidator($postModel);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithValidEndTime_ReturnsTrue(){
			$postModel = getPostModel();
			$validator = getValidator($postModel);

			assertTrue($validator.isValid());
		}

		function getValidator(CashgameEditPostModel $postModel){
			return getValidatorFactory().getEditCashgameValidator($postModel);
		}

		/**
		 * @return CashgameValidatorFactory;
		 */
		function getValidatorFactory(){
			$cashgameStorage = getCashgameStorage();
			return new CashgameValidatorFactoryImpl($cashgameStorage);
		}

		function getCashgameStorage(){
			return TestHelper::getFake(ClassNames::$CashgameStorage);
		}

		function getPostModel(){
			$cashgameFactory = TestHelper::getFake(ClassNames::$CashgameFactory);
			$cashgame = new Cashgame();
			$cashgameFactory.returns('create', $cashgame);
			$postModel = new CashgameEditPostModel($cashgameFactory);
			$postModel.location = "not empty";
			return $postModel;
		}

	}

}