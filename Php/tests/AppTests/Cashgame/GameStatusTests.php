namespace tests\AppTests\Cashgame{

	use tests\UnitTestCase;
	use entities\GameStatus;
	use tests\TestHelper;

	class GameStatusTests extends UnitTestCase {

		function test_GetName_WithCreatedStatus_IsSetToCreated(){
			$sut = GameStatus::getName(GameStatus::created);

			assertIdentical("Created", $sut);
		}

		function test_GetName_WithRunningStatus_IsSetToRunning(){
			$sut = GameStatus::getName(GameStatus::running);

			assertIdentical("Running", $sut);
		}

		function test_GetName_WithFinishedStatus_IsSetToFinished(){
			$sut = GameStatus::getName(GameStatus::finished);

			assertIdentical("Finished", $sut);
		}

		function test_GetName_WithPublishedStatus_IsSetToPublished(){
			$sut = GameStatus::getName(GameStatus::published);

			assertIdentical("Published", $sut);
		}

	}

}