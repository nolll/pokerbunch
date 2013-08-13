namespace entities{

	class PlayerFactoryImpl implements PlayerFactory{

		public function create($displayName, $role = null, $userName = null, $id = null){
			if($role == null){
				$role = Role::$player;
			}
			$player = new Player();
			$player->setDisplayName($displayName);
			$player->setRole($role);
			$player->setUserName($userName);
			$player->setId($id);
			return $player;
		}

	}

}