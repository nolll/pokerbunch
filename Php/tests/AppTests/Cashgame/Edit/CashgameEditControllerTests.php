<?php
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
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->cashgameValidatorFactory = TestHelper::getFake(ClassNames::$CashgameValidatorFactory);
			$this->postModel = TestHelper::getFake('app\Cashgame\Edit\CashgameEditPostModel');
			$this->sut = new CashgameEditController($this->userContext, $this->homegameRepositoryMock, $this->cashgameRepositoryMock, $this->cashgameValidatorFactory, $this->postModel);
		}

		function test_ActionEdit_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requireManager');
			$this->expectException();

			$this->sut->action_edit("homegame1", "2010-01-01");
		}

		function test_ActionEdit_ReturnsModelOfCorrectType(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$cashgame = new Cashgame();
			$this->cashgameRepositoryMock->returns('getByDate', $cashgame);
			$this->cashgameRepositoryMock->returns('getLocations', array());

			$viewResult = $this->sut->action_edit("homegame1", "2010-01-01");

			$this->assertIdentical('app\Cashgame\Edit\CashgameEditModel', get_class($viewResult->model));
		}

		function test_ActionEditPost_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requireManager');
			$this->expectException();

			$this->sut->action_edit_post("homegame1", "2010-01-01");
		}

		function test_ActionEditPost_WithValidValues_CallsUpdateGame(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$cashgame = new Cashgame();
			$this->cashgameRepositoryMock->returns('getByDate', $cashgame);
			$this->setupValidCashgameValidator();
			$this->postModel->returns('getCashgame', $cashgame);

			$this->cashgameRepositoryMock->expectOnce("updateGame");

			$this->sut->action_edit_post("homegame1", "2010-01-01");
		}

		function test_ActionEditPost_WithValidValues_RedirectsToDetails(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$cashgame = new Cashgame();
			$this->cashgameRepositoryMock->returns('getByDate', $cashgame);
			$this->setupValidCashgameValidator();
			$this->postModel->returns('getCashgame', $cashgame);

			$urlModel = $this->sut->action_edit_post("homegame1", "2010-01-01");

			$this->assertIsA($urlModel, 'app\Urls\CashgameDetailsUrlModel');
		}

		function test_ActionEditPost_WithInvalidValues_DoesNotCallUpdateGame(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$cashgame = new Cashgame();
			$this->cashgameRepositoryMock->returns('getByDate', $cashgame);
			$this->cashgameRepositoryMock->returns('getLocations', array());
			$this->setupInvalidCashgameValidator();
			$this->postModel->returns('getCashgame', $cashgame);

			$this->cashgameRepositoryMock->expectNever("updateGame");

			$this->sut->action_edit_post("homegame1", "2010-01-01");
		}

		function test_ActionDelete_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requireManager');
			$this->expectException();

			$this->sut->action_delete("homegame1", "2010-01-01");
		}

		function test_ActionDelete_WithManagerRights_CallsDeleteAndRedirectsToList(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$cashgame = new Cashgame();
			$this->cashgameRepositoryMock->returns('getByDate', $cashgame);

			$this->cashgameRepositoryMock->expectOnce("deleteGame");

			$urlModel = $this->sut->action_delete("homegame1", "2010-01-01");

			$this->assertIsA($urlModel, 'app\Urls\CashgameListingUrlModel');
		}

		function setupValidCashgameValidator(){
			$validator = new ValidatorFake(true);
			$this->setupCashgameValidator($validator);
		}

		function setupInvalidCashgameValidator(){
			$validator = new ValidatorFake(false);
			$this->setupCashgameValidator($validator);
		}

		function setupCashgameValidator(Validator $validator){
			$this->cashgameValidatorFactory->returns("getEditCashgameValidator", $validator);
		}

	}

}