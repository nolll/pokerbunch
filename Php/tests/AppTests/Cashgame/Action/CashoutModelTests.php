namespace tests\AppTests\Cashgame\Action{

	use DateTime;
	use entities\Cashgame;
	use entities\Homegame;
	use entities\Player;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\Cashgame\Action\CashoutModel;
	use tests\TestHelper;

	class CashoutModelTests extends UnitTestCase {

		private $homegame;
		/** @var Player */
		private $player;
		private $postedAmount;

		function setUp(){
			homegame = new Homegame();
			player = new Player();
			postedAmount = null;
		}

		function getSut(){
			$runningGame = new Cashgame();
			$runningGame.setStartTime(new DateTime());
			return new CashoutModel(new User(), homegame, player, null, $runningGame, postedAmount);
		}

		function test_CashoutUrl_IsSet(){
			$sut = getSut();

			assertIsA($sut.cashoutUrl, 'app\Urls\CashgameCashoutUrlModel');
		}

		function test_CashoutAmount_WithPostedAmount_IsSetToEmptyString(){
			$sut = getSut();

			assertEqual($sut.cashoutAmount, '');
		}

		function test_CashoutAmount_WithPostedAmount_IsSet(){
			postedAmount = 1;

			$sut = getSut();

			assertEqual($sut.cashoutAmount, 1);
		}

	}

}