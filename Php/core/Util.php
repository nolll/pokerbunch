<?php
namespace core{

	class Util{

		public static function formatWinnings($winnings){
			if($winnings > 0){
				return '+' . $winnings;
			}
			return $winnings;
		}

		public static function getWinningsCssClass($winnings){
			if($winnings > 0){
				return 'pos-result';
			} else if ($winnings < 0) {
				return 'neg-result';
			} else {
				return '';
			}
		}

		public static function startsWith($haystack, $needle){
			return strpos($haystack, $needle) === 0;
		}

		public static function endsWith($haystack, $needle){
			$length = strlen($needle);
			if ($length == 0) {
				return true;
			}
			return (substr($haystack, -$length) === $needle);
		}

		public static function contains($haystack, $needle){
			$result = strpos($haystack, $needle);
			return $result !== false;
		}

		public static function isInteger($int){
			if(is_numeric($int)){
				if((int)$int == $int){
					return true;
				} else {
					return false;
				}
			} else {
				return false;
			}
		}

	}

}