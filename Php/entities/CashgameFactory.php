<?php
namespace entities{

	interface CashgameFactory{

		public function create($location, $status = null, $id = null, array $results = null);

	}

}