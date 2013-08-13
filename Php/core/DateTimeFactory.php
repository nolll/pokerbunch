namespace core{
	use DateTime;
	use DateTimeZone;

	class DateTimeFactory{

		public static function now(DateTimeZone $timezone = null){
			return self::create(null, $timezone);
		}

		public static function create($str, DateTimeZone $timezone = null){
			if($timezone == null){
				return new DateTime($str);
			}
			return new DateTime($str, $timezone);
		}

		public static function toUtc(DateTime $dateTime){
			$utc = clone $dateTime;
			$utc->setTimezone(new DateTimeZone('UTC'));
			return $utc;
		}

	}

}