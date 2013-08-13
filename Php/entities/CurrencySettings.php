namespace entities{

	class CurrencySettings{

		private $symbol;
		private $layout;

		public function __construct($symbol = null, $layout = null){
			$this->symbol = $symbol;
			$this->layout = $layout;
		}

		public function getSymbol(){
			return $this->symbol;
		}

		public function setSymbol($symbol){
			$this->symbol = $symbol;
		}

		public function getLayout(){
			return $this->layout;
		}

		public function setLayout($layout){
			$this->layout = $layout;
		}

	}

}