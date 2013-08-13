namespace tests\StorageTests{

	use core\ClassNames;
	use entities\GameStatus;
	use Infrastructure\Data\MySql\MySqlCashgameStorage;
	use Infrastructure\Data\Classes\RawCashgame;
	use Infrastructure\Data\MySql\PreparedStatement;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class MySqlCashgameStorageTests extends UnitTestCase {

		/** @var MySqlCashgameStorage */
		private $sut;
		private $storageProvider;

		function setUp(){
			$this->storageProvider = TestHelper::getFake(ClassNames::$StorageProvider);
			$this->sut = new MySqlCashgameStorage($this->storageProvider);
		}

		function test_UpdateGame_CallsExecuteWithCorrectSql(){
			$id = 1;
			$location = 'a';
			$status = GameStatus::created;
			$date = "2001-01-01";
			$cashgame = new RawCashgame($id, $location, $status, $date);
			$expectedSql = PreparedStatement::UpdateCashgame;

			$this->storageProvider->expectOnce("executePrepared", array($expectedSql, $location, $date, $status, $id));

			$this->sut->updateGame($cashgame);
		}

	}

}