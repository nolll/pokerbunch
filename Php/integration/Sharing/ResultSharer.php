<?php
namespace integration\Sharing{

	use entities\Cashgame;

	interface ResultSharer {

		public function shareResult(Cashgame $cashgame);

	}

}