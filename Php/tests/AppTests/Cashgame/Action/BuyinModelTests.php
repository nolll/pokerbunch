namespace tests\AppTests\Cashgame\Action{

	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Homegame;
	use entities\Player;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\Cashgame\Action\BuyinModel;
	use tests\TestHelper;

	class BuyinModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;
		/** @var Player */
		private $player;
		/** @var Cashgame */
		private $cashgame;
		private $postedValue;

		function setUp(){
			homegame = new Homegame();
			player = new Player();
			cashgame = new Cashgame();
			postedValue = null;
		}

		function getSut(){
			return new BuyinModel(new User(), homegame, player, null, cashgame, postedValue);
		}

		function test_BuyinUrl_IsSet(){
			$sut = getSut();

			assertIsA($sut.buyinUrl, 'app\Urls\CashgameBuyinUrlModel');
		}

		function test_StackFieldEnabled_WithPlayerInGame_IsTrue(){
			player.setId(1);
			$cashgameResult = new CashgameResult();
			$cashgameResult.setPlayer(player);
			cashgame.setResults(array($cashgameResult));

			$sut = getSut();

			assertTrue($sut.stackFieldEnabled);
		}

		function test_StackFieldEnabled_WithPlayerNotInGame_IsFalse(){
			player.setId(2);

			$sut = getSut();

			assertFalse($sut.stackFieldEnabled);
		}

		function test_BuyinAmount_WithoutPostedValue_IsSetToDefaultBuyin(){
			homegame.setDefaultBuyin(1);

			$sut = getSut();

			assertIdentical(1, $sut.buyinAmount);
		}

		function test_BuyinAmount_WithPostedValue_IsSetToPostedValue(){
			homegame.setDefaultBuyin(1);
			postedValue = 2;

			$sut = getSut();

			assertIdentical(2, $sut.buyinAmount);
		}

	}

}