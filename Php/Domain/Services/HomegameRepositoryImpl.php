namespace Domain\Services {

	use DateTimeZone;
	use Domain\Interfaces\HomegameRepository;
	use entities\CurrencySettings;
	use entities\Homegame;
	use Infrastructure\Data\Classes\RawHomegame;
	use Infrastructure\Data\Interfaces\HomegameStorage;

	class HomegameRepositoryImpl implements HomegameRepository{

		private $homegameStorage;

		public function __construct(HomegameStorage $homegameStorage){
			$this->homegameStorage = $homegameStorage;
		}

		/**
		 * @param $name
		 * @return Homegame
		 */
		public function getByName($name){
			$rawHomegame = $this->homegameStorage->getRawHomegameByName($name);
			if($rawHomegame == null){
				return null;
			}
			return $this->getHomegameFromRawHomegame($rawHomegame);
		}

		private function getHomegameFromRawHomegame(RawHomegame $rawHomegame){
			$homegame = new Homegame();
			$homegame->setId($rawHomegame->getId());
			$homegame->setSlug($rawHomegame->getSlug());
			$homegame->setDisplayName($rawHomegame->getDisplayName());
			$homegame->setDescription($rawHomegame->getDescription());
			$homegame->setHouseRules($rawHomegame->getHouseRules());
			$homegame->setCurrency(new CurrencySettings($rawHomegame->getCurrencySymbol(), $rawHomegame->getCurrencyLayout()));
			$homegame->setTimezone(new DateTimeZone($rawHomegame->getTimezoneName()));
			$homegame->setDefaultBuyin($rawHomegame->getDefaultBuyin());
			$homegame->cashgamesEnabled = $rawHomegame->cashgamesEnabled;
			$homegame->tournamentsEnabled = $rawHomegame->tournamentsEnabled;
			$homegame->videosEnabled = $rawHomegame->videosEnabled;
			return $homegame;
		}

	}

}