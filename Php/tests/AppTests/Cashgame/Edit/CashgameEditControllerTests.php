namespace tests\AppTests\Cashgame\Edit{

	use app\Cashgame\Edit\CashgameEditController;
	use core\Validation\ValidatorFake;
	use entities\Cashgame;
	use entities\Homegame;
	use core\ClassNames;
	use tests\TestHelper;
	use core\Validation\Validator;
	use tests\UnitTestCase;

	class CashgameEditControllerTests extends UnitTestCase {

		/** @var CashgameEditController */
		private $sut;
		private $userContext;
		private $cashgameValidatorFactory;
		private $postModel;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			cashgameValidatorFactory = TestHelper::getFake(ClassNames::$CashgameValidatorFactory);
			postModel = TestHelper::getFake('app\Cashgame\Edit\CashgameEditPostModel');
			sut = new CashgameEditController(userContext, homegameRepositoryMock, cashgameRepositoryMock, cashgameValidatorFactory, postModel);
		}

		function test_ActionEdit_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requireManager');
			expectException();

			sut.action_edit("homegame1", "2010-01-01");
		}

		function test_ActionEdit_ReturnsModelOfCorrectType(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			cashgameRepositoryMock.returns('getByDate', $cashgame);
			cashgameRepositoryMock.returns('getLocations', array());

			$viewResult = sut.action_edit("homegame1", "2010-01-01");

			assertIdentical('app\Cashgame\Edit\CashgameEditModel', get_class($viewResult.model));
		}

		function test_ActionEditPost_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requireManager');
			expectException();

			sut.action_edit_post("homegame1", "2010-01-01");
		}

		function test_ActionEditPost_WithValidValues_CallsUpdateGame(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			cashgameRepositoryMock.returns('getByDate', $cashgame);
			setupValidCashgameValidator();
			postModel.returns('getCashgame', $cashgame);

			cashgameRepositoryMock.expectOnce("updateGame");

			sut.action_edit_post("homegame1", "2010-01-01");
		}

		function test_ActionEditPost_WithValidValues_RedirectsToDetails(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			cashgameRepositoryMock.returns('getByDate', $cashgame);
			setupValidCashgameValidator();
			postModel.returns('getCashgame', $cashgame);

			$urlModel = sut.action_edit_post("homegame1", "2010-01-01");

			assertIsA($urlModel, 'app\Urls\CashgameDetailsUrlModel');
		}

		function test_ActionEditPost_WithInvalidValues_DoesNotCallUpdateGame(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			cashgameRepositoryMock.returns('getByDate', $cashgame);
			cashgameRepositoryMock.returns('getLocations', array());
			setupInvalidCashgameValidator();
			postModel.returns('getCashgame', $cashgame);

			cashgameRepositoryMock.expectNever("updateGame");

			sut.action_edit_post("homegame1", "2010-01-01");
		}

		function test_ActionDelete_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requireManager');
			expectException();

			sut.action_delete("homegame1", "2010-01-01");
		}

		function test_ActionDelete_WithManagerRights_CallsDeleteAndRedirectsToList(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			cashgameRepositoryMock.returns('getByDate', $cashgame);

			cashgameRepositoryMock.expectOnce("deleteGame");

			$urlModel = sut.action_delete("homegame1", "2010-01-01");

			assertIsA($urlModel, 'app\Urls\CashgameListUrlModel');
		}

		function setupValidCashgameValidator(){
			$validator = new ValidatorFake(true);
			setupCashgameValidator($validator);
		}

		function setupInvalidCashgameValidator(){
			$validator = new ValidatorFake(false);
			setupCashgameValidator($validator);
		}

		function setupCashgameValidator(Validator $validator){
			cashgameValidatorFactory.returns("getEditCashgameValidator", $validator);
		}

	}

}