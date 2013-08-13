<?php
namespace app\Cashgame\Matrix{

	use entities\Homegame;
	use Domain\Classes\User;

	interface MatrixModelFactory {

		public function get(Homegame $homegame, User $user, $year = null);

	}

}