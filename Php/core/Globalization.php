namespace core{

	use DateTimeZone;
	use DateTime;
	use entities\CurrencySettings;

	class Globalization{

		public static function formatNumber($number){
			return number_format($number, 0, '.', ' ');
		}

		public static function formatCurrency(CurrencySettings $currency, $amount){
			$numberFormatted = self::formatNumber($amount);
			$amountFormatted = str_replace('{AMOUNT}', $numberFormatted, $currency.getLayout());
			$currencyFormatted = str_replace('{SYMBOL}', $currency.getSymbol(), $amountFormatted);
			return $currencyFormatted;
		}

		public static function formatWinrate(CurrencySettings $currency, $winrate){
			return self::formatCurrency($currency, $winrate) . '/h';
		}

		public static function formatResult(CurrencySettings $currency, $result){
			$currency = self::formatCurrency($currency, $result);
			if($result > 0){
				return '+' . $currency;
			} else {
				return $currency;
			}
		}

		public static function formatDuration($minutes){
			$h = floor($minutes / 60);
			$m = $minutes % 60;
			if($h > 0 && $m > 0){
				return $h . 'h ' . $m . 'm';
			} else if ($h > 0){
				return $h . 'h';
			} else {
				return $m . 'm';
			}
		}

		public static function formatTimespan($secondsAgo){
			$minutes = round($secondsAgo / 60);
			if($minutes == 0){
				return 'now';
			}
			if($minutes == 1){
				return '1 minute';
			}
			return $minutes . ' minutes';
		}

		public static function formatShortDate(DateTime $date, $includeYear = false){
			return $date.format(self::getShortDateFormat($includeYear));
		}

		public static function formatShortDateTime(DateTime $date, $includeYear = false){
			return $date.format(self::getShortDateTimeFormat($includeYear));
		}

		public static function formatTime(DateTime $date){
			return $date.format('H:i');
		}

		public static function formatIsoDate(DateTime $date){
			return $date.format('Y-m-d');
		}

		public static function formatIsoDateTime(DateTime $date){
			return $date.format('Y-m-d H:i:s');
		}

		public static function formatYear(DateTime $date){
			return $date.format('Y');
		}

		public static function getTimezoneNames(){
			return array_values(array_diff(DateTimeZone::listIdentifiers(), self::getInvalidTimezoneNames()));
		}

		public static function getDefaultTimezoneName(){
			return 'America/New_York';
		}

		public static function getDefaultCurrency(){
			return '$';
		}

		public static function getDefaultCurrencyLayout(){
			return '{SYMBOL}{AMOUNT}';
		}

		public static function getCurrencyLayouts(){
			return Array(
				'{SYMBOL} {AMOUNT}',
				'{SYMBOL}{AMOUNT}',
				'{AMOUNT}{SYMBOL}',
				'{AMOUNT} {SYMBOL}'
			);
		}

		private static function getShortDateFormat($includeYear = false){
			if ($includeYear){
				return 'M j Y';
			} else {
				return 'M j';
			}
		}

		private static function getShortDateTimeFormat($includeYear = false){
			if ($includeYear){
				return 'M j Y H:i';
			} else {
				return 'M j H:i';
			}
		}

		private static function getInvalidTimezoneNames(){
			return Array(
						'Brazil/Acre','Brazil/DeNoronha','Brazil/East','Brazil/West','Canada/Atlantic','Canada/Central',
						'Canada/East-Saskatchewan','Canada/Eastern','Canada/Mountain','Canada/Newfoundland','Canada/Pacific',
						'Canada/Saskatchewan','Canada/Yukon','CET','Chile/Continental','Chile/EasterIsland','CST6CDT','Cuba',
						'EET','Egypt','Eire','EST','EST5EDT','Etc/GMT','Etc/GMT+0','Etc/GMT+1','Etc/GMT+10','Etc/GMT+11',
						'Etc/GMT+12','Etc/GMT+2','Etc/GMT+3','Etc/GMT+4','Etc/GMT+5','Etc/GMT+6','Etc/GMT+7','Etc/GMT+8',
						'Etc/GMT+9','Etc/GMT-0','Etc/GMT-1','Etc/GMT-10','Etc/GMT-11','Etc/GMT-12','Etc/GMT-13','Etc/GMT-14',
						'Etc/GMT-2','Etc/GMT-3','Etc/GMT-4','Etc/GMT-5','Etc/GMT-6','Etc/GMT-7','Etc/GMT-8','Etc/GMT-9',
						'Etc/GMT0','Etc/Greenwich','Etc/UCT','Etc/Universal','Etc/UTC','Etc/Zulu','Factory','GB','GB-Eire',
						'GMT','GMT+0','GMT-0','GMT0','Greenwich','Hongkong','HST','Iceland','Iran','Israel','Jamaica','Japan',
						'Kwajalein','Libya','MET','Mexico/BajaNorte','Mexico/BajaSur','Mexico/General','MST','MST7MDT','Navajo',
						'NZ','NZ-CHAT','Poland','Portugal','PRC','PST8PDT','ROC','ROK','Singapore','Turkey','UCT','Universal',
						'US/Alaska','US/Aleutian','US/Arizona','US/Central','US/East-Indiana','US/Eastern','US/Hawaii',
						'US/Indiana-Starke','US/Michigan','US/Mountain','US/Pacific','US/Pacific-New','US/Samoa','W-SU','WET','Zulu'
			);
		}
	}
}