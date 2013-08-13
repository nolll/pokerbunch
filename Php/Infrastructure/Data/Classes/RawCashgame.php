<?php
namespace Infrastructure\Data\Classes {

	use Infrastructure\Data\Classes\RawCashgameResult;

	class RawCashgame{

		private $location;
		/** @var RawCashgameResult[] */
		private $results;
		private $id;
		private $status;
		private $date;

		public function __construct($id, $location, $status, $date){
			$this->results = array();
			$this->id = $id;
			$this->location = $location;
			$this->status = $status;
			$this->date = $date;
		}

		public function getId(){
			return $this->id;
		}

		public function getLocation(){
			return $this->location;
		}

		public function getStatus(){
			return $this->status;
		}

		public function getDate(){
			return $this->date;
		}

		/**
		 * @param RawCashgameResult $result
		 * @return void
		 */
		public function addResult(RawCashgameResult $result){
			$this->results[] = $result;
		}

		/**
		 * @return RawCashgameResult[]
		 */
		public function getResults(){
			return $this->results;
		}

	}

}