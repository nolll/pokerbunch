<?php
namespace tests\AppTests\Cashgame{

	use tests\UnitTestCase;
	use entities\GameStatus;
	use tests\TestHelper;

	class GameStatusTests extends UnitTestCase {

		function test_GetName_WithCreatedStatus_IsSetToCreated(){
			$sut = GameStatus::getName(GameStatus::created);

			$this->assertIdentical("Created", $sut);
		}

		function test_GetName_WithRunningStatus_IsSetToRunning(){
			$sut = GameStatus::getName(GameStatus::running);

			$this->assertIdentical("Running", $sut);
		}

		function test_GetName_WithFinishedStatus_IsSetToFinished(){
			$sut = GameStatus::getName(GameStatus::finished);

			$this->assertIdentical("Finished", $sut);
		}

		function test_GetName_WithPublishedStatus_IsSetToPublished(){
			$sut = GameStatus::getName(GameStatus::published);

			$this->assertIdentical("Published", $sut);
		}

	}

}