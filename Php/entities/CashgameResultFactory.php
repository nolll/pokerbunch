<?php
namespace entities{

	interface CashgameResultFactory{

		public function create(Player $player, array $checkpoints);

	}

}