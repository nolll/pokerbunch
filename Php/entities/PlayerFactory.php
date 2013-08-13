<?php
namespace entities{

	interface PlayerFactory{

		/**
		 * @param $displayName
		 * @param null $role
		 * @param null $userName
		 * @param null $id
		 * @return Player
		 */
		public function create($displayName, $role = null, $userName = null, $id = null);

	}

}