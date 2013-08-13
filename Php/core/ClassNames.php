namespace core{

	class ClassNames{

		public static $AvatarService = 'integration\Avatar\AvatarService';
		public static $GravatarService = 'integration\Avatar\GravatarService';

		public static $CashgameFactory = 'entities\CashgameFactory';
		public static $CashgameFactoryImpl = 'entities\CashgameFactoryImpl';

		public static $CashgameSuiteFactory = 'entities\CashgameSuiteFactory';
		public static $CashgameSuiteFactoryImpl = 'entities\CashgameSuiteFactoryImpl';

		public static $CashgameResultFactory = 'entities\CashgameResultFactory';
		public static $CashgameResultFactoryImpl = 'entities\CashgameResultFactoryImpl';

		public static $CashgameTotalResultFactory = 'entities\CashgameTotalResultFactory';
		public static $CashgameTotalResultFactoryImpl = 'entities\CashgameTotalResultFactoryImpl';

		public static $CashgameValidatorFactory = 'app\Cashgame\CashgameValidatorFactory';
		public static $CashgameValidatorFactoryImpl = 'app\Cashgame\CashgameValidatorFactoryImpl';

		public static $Encryption = 'app\User\Encryption';
		public static $EncryptionImpl = 'app\User\EncryptionImpl';

		public static $HomegameValidatorFactory = 'app\Homegame\HomegameValidatorFactory';
		public static $HomegameValidatorFactoryImpl = 'app\Homegame\HomegameValidatorFactoryImpl';
		public static $InvitationCodeCreator = 'app\Player\InvitationCodeCreator';
		public static $InvitationCodeCreatorImpl = 'app\Player\InvitationCodeCreatorImpl';

		public static $InvitationSender = 'app\Player\InvitationSender';
		public static $InvitationSenderImpl = 'app\Player\InvitationSenderImpl';

		public static $MessageSenderFactory = 'integration\Message\MessageSenderFactory';
		public static $MessageSenderFactoryImpl = 'integration\Message\MessageSenderFactoryImpl';

		public static $OutputSettings = 'Mishiin\OutputSettings';

		public static $PasswordGenerator = 'app\User\PasswordGenerator';

		public static $PasswordSender = 'app\User\ForgotPassword\PasswordSender';
		public static $PasswordSenderImpl = 'app\User\ForgotPassword\PasswordSenderImpl';

		public static $PlayerValidatorFactory = 'app\Player\PlayerValidatorFactory';
		public static $PlayerValidatorFactoryImpl = 'app\Player\PlayerValidatorFactoryImpl';

		public static $RegistrationConfirmationSender = 'app\User\Add\RegistrationConfirmationSender';
		public static $RegistrationConfirmationSenderImpl = 'app\User\Add\RegistrationConfirmationSenderImpl';

		public static $Request = 'Mishiin\Request';
		public static $Response = 'Mishiin\Response';

		public static $ResultSharer = 'integration\Sharing\ResultSharer';
		public static $ResultSharerImpl = 'integration\Sharing\ResultSharerImpl';

		public static $Settings = 'config\Settings';

		public static $SlugGenerator = 'app\Homegame\SlugGenerator';
		public static $SlugGeneratorImpl = 'app\Homegame\SlugGeneratorImpl';

		public static $SocialServiceFactory = 'integration\Sharing\SocialServiceFactory';
		public static $SocialServiceFactoryImpl = 'integration\Sharing\SocialServiceFactoryImpl';

		public static $TemplateEngine = 'Mishiin\TemplateEngine';

		public static $MishiinConfigFactory = 'Mishiin\ConfigFactory';
		public static $AppConfigFactory = 'config\ConfigFactory';

		public static $TwitterService = 'integration\Sharing\TwitterService';
		public static $TwitterServiceImpl = 'integration\Sharing\TwitterServiceImpl';

		public static $UserContext = 'core\UserContext';
		public static $UserContextImpl = 'core\UserContextImpl';

		public static $WebContext = 'core\WebContext';
		public static $WebContextImpl = 'core\WebContextImpl';

		public static $Timer = 'core\Timer';
		public static $TimerImpl = 'core\TimerImpl';

		public static $UserFactory = 'app\User\UserFactory';
		public static $UserFactoryImpl = 'app\User\UserFactoryImpl';

		public static $PlayerFactory = 'entities\PlayerFactory';
		public static $PlayerFactoryImpl = 'entities\PlayerFactoryImpl';

		public static $UserValidatorFactory = 'app\User\UserValidatorFactory';
		public static $UserValidatorFactoryImpl = 'app\User\UserValidatorFactoryImpl';

		public static $MatrixModelFactory = 'app\Cashgame\Matrix\MatrixModelFactory';
		public static $MatrixModelFactoryImpl = 'app\Cashgame\Matrix\MatrixModelFactoryImpl';

		// Domain Services
		public static $CashgameRepository = 'Domain\Interfaces\CashgameRepository';
		public static $CashgameRepositoryImpl = 'Domain\Services\CashgameRepositoryImpl';
		public static $HomegameRepository = 'Domain\Interfaces\HomegameRepository';
		public static $HomegameRepositoryImpl = 'Domain\Services\HomegameRepositoryImpl';
		public static $PlayerRepository = 'Domain\Interfaces\PlayerRepository';
		public static $PlayerRepositoryImpl = 'Domain\Services\PlayerRepositoryImpl';

		// Infrastructure
		public static $StorageProvider = 'Infrastructure\Data\MySql\StorageProvider';
		public static $MySqlPdo = 'Infrastructure\Data\MySql\MySqlPdo';
		public static $CashgameStorage = 'Infrastructure\Data\Interfaces\CashgameStorage';
		public static $MySqlCashgameStorage = 'Infrastructure\Data\MySql\MySqlCashgameStorage';
		public static $HomegameStorage = 'Infrastructure\Data\Interfaces\HomegameStorage';
		public static $MySqlHomegameStorage = 'Infrastructure\Data\MySql\MySqlHomegameStorage';
		public static $PlayerStorage = 'Infrastructure\Data\Interfaces\PlayerStorage';
		public static $MySqlPlayerStorage = 'Infrastructure\Data\MySql\MySqlPlayerStorage';
		public static $SharingStorage = 'Infrastructure\Data\Interfaces\SharingStorage';
		public static $MySqlSharingStorage = 'Infrastructure\Data\MySql\MySqlSharingStorage';
		public static $TwitterStorage = 'Infrastructure\Data\Interfaces\TwitterStorage';
		public static $MySqlTwitterStorage = 'Infrastructure\Data\MySql\MySqlTwitterStorage';
		public static $UserStorage = 'Infrastructure\Data\Interfaces\UserStorage';
		public static $MySqlUserStorage = 'Infrastructure\Data\MySql\MySqlUserStorage';

		public static $Logger = 'Infrastructure\Logging\Logger';
		public static $DumpLogger = 'Infrastructure\Logging\DumpLogger';

	}

}