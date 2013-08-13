namespace app\Cashgame\Action{

	use Mishiin\Request;
	use app\Urls\CashgameCheckpointDeleteUrlModel;
	use entities\Cashgame;
	use entities\Player;
	use entities\Role;
	use entities\Homegame;
	use core\Globalization;
	use entities\Checkpoints\Checkpoint;

	class CheckpointModel {

		public $description;
		public $stack;
		public $timestamp;
		public $showLink;
		public $editUrl;

		public function __construct(Homegame $homegame, Cashgame $cashgame, Player $player, Checkpoint $checkpoint, $role){
			$this->description = $checkpoint->getDescription();
			$this->stack = Globalization::formatCurrency($homegame->getCurrency(), $checkpoint->getStack());
			$this->timestamp = Globalization::formatTime($checkpoint->getTimestamp());
			$this->showLink = $role >= Role::$manager;
			$this->editUrl = new CashgameCheckpointDeleteUrlModel($homegame, $cashgame, $player, $checkpoint);
		}

	}

}