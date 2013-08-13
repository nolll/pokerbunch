namespace tests\AppTests\Cashgame\Listing{

	use entities\Cashgame;
	use entities\Homegame;
	use tests\UnitTestCase;
	use DateTime;
	use entities\GameStatus;
	use app\Cashgame\Listing\CashgameTable\CashgameTableModel;
	use tests\TestHelper;

	class CashgameTableModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;
		private $cashgames;

		function setUp(){
			$this->homegame = new Homegame();
			$this->cashgames = array();
		}

		function test_Homegame_IsSet(){
			$model = $this->getModel();
			$this->assertIsA($model->homegame, 'entities\Homegame');
		}

		function test_Cashgames_IsSet(){
			$this->cashgames = $this->getCashgames();
			$model = $this->getModel();
			$this->assertEqual(3, count($model->cashgames));
		}

		function test_Table_WithOneCashgame_OneItemIsCorrectType(){
			$this->cashgames = $this->getCashgames();

			$model = $this->getModel();

			$this->assertIsA($model->listItemModels[0], 'app\Cashgame\Listing\CashgameTableItem\CashgameTableItemModel');
			$this->assertEqual(3, count($model->listItemModels));
		}

		function getModel(){
			return new CashgameTableModel($this->homegame, $this->cashgames);
		}

		function getCashgames(){
			$cashgame1 = new Cashgame();
			$cashgame1->setStatus(GameStatus::finished);
			$cashgame1->setStartTime(new DateTime('2010-01-01 01:00:00'));
			$cashgame2 = new Cashgame();
			$cashgame2->setStatus(GameStatus::published);
			$cashgame2->setStartTime(new DateTime('2010-01-02 01:00:00'));
			$cashgame3 = new Cashgame();
			$cashgame3->setStatus(GameStatus::published);
			$cashgame3->setStartTime(new DateTime('2011-01-01 01:00:00'));
			return array($cashgame1, $cashgame2, $cashgame3);
		}

	}

}