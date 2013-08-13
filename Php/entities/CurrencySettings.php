namespace entities{

	class CurrencySettings{

		private $symbol;
		private $layout;

		public function __construct($symbol = null, $layout = null){
			symbol = $symbol;
			layout = $layout;
		}

		public function getSymbol(){
			return symbol;
		}

		public function setSymbol($symbol){
			symbol = $symbol;
		}

		public function getLayout(){
			return layout;
		}

		public function setLayout($layout){
			layout = $layout;
		}

	}

}