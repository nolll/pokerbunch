namespace app{

	use core\ClassNames;
	use Mishiin\RequestProvider;
	use Mishiin\Response;

	class ApplicationModule extends MishiinModule {

		public function configure() {
			parent::configure();
			parent::bindTemplateEngine();
			parent::bindParamParser('app\AppParamParser');

			bind(ClassNames::$MishiinConfigFactory).to(ClassNames::$AppConfigFactory).inSingleton();
			bind(ClassNames::$UserContext).to(ClassNames::$UserContextImpl).inSingleton();
			bind(ClassNames::$WebContext).to(ClassNames::$WebContextImpl).inSingleton();
			bind(ClassNames::$Timer).to(ClassNames::$TimerImpl).inSingleton();

			bind(ClassNames::$SlugGenerator).to(ClassNames::$SlugGeneratorImpl).inSingleton();
			bind(ClassNames::$HomegameValidatorFactory).to(ClassNames::$HomegameValidatorFactoryImpl).inSingleton();

			bind(ClassNames::$CashgameValidatorFactory).to(ClassNames::$CashgameValidatorFactoryImpl).inSingleton();

			bind(ClassNames::$PlayerValidatorFactory).to(ClassNames::$PlayerValidatorFactoryImpl).inSingleton();
			bind(ClassNames::$InvitationSender).to(ClassNames::$InvitationSenderImpl).inSingleton();
			bind(ClassNames::$InvitationCodeCreator).to(ClassNames::$InvitationCodeCreatorImpl).inSingleton();

			bind(ClassNames::$Encryption).to(ClassNames::$EncryptionImpl).inSingleton();
			bind(ClassNames::$UserFactory).to(ClassNames::$UserFactoryImpl).inSingleton();
			bind(ClassNames::$UserValidatorFactory).to(ClassNames::$UserValidatorFactoryImpl).inSingleton();
			bind(ClassNames::$RegistrationConfirmationSender).to(ClassNames::$RegistrationConfirmationSenderImpl).inSingleton();
			bind(ClassNames::$PasswordSender).to(ClassNames::$PasswordSenderImpl).inSingleton();

			bind(ClassNames::$CashgameFactory).to(ClassNames::$CashgameFactoryImpl).inSingleton();
			bind(ClassNames::$CashgameSuiteFactory).to(ClassNames::$CashgameSuiteFactoryImpl).inSingleton();
			bind(ClassNames::$CashgameResultFactory).to(ClassNames::$CashgameResultFactoryImpl).inSingleton();
			bind(ClassNames::$CashgameTotalResultFactory).to(ClassNames::$CashgameTotalResultFactoryImpl).inSingleton();

			bind(ClassNames::$HomegameRepository).to(ClassNames::$HomegameRepositoryImpl).inSingleton();
			bind(ClassNames::$CashgameRepository).to(ClassNames::$CashgameRepositoryImpl).inSingleton();
			bind(ClassNames::$PlayerRepository).to(ClassNames::$PlayerRepositoryImpl).inSingleton();
			bind(ClassNames::$PlayerFactory).to(ClassNames::$PlayerFactoryImpl).inSingleton();

			bind(ClassNames::$SocialServiceFactory).to(ClassNames::$SocialServiceFactoryImpl).inSingleton();
			bind(ClassNames::$TwitterService).to(ClassNames::$TwitterServiceImpl).inSingleton();
			bind(ClassNames::$ResultSharer).to(ClassNames::$ResultSharerImpl).inSingleton();
			bind(ClassNames::$MessageSenderFactory).to(ClassNames::$MessageSenderFactoryImpl).inSingleton();
			bind(ClassNames::$AvatarService).to(ClassNames::$GravatarService).inSingleton();

			bind(ClassNames::$MatrixModelFactory).to(ClassNames::$MatrixModelFactoryImpl).inSingleton();

			bind(ClassNames::$StorageProvider).to(ClassNames::$MySqlPdo).inSingleton();
			bind(ClassNames::$UserStorage).to(ClassNames::$MySqlUserStorage).inSingleton();
			bind(ClassNames::$HomegameStorage).to(ClassNames::$MySqlHomegameStorage).inSingleton();
			bind(ClassNames::$PlayerStorage).to(ClassNames::$MySqlPlayerStorage).inSingleton();
			bind(ClassNames::$CashgameStorage).to(ClassNames::$MySqlCashgameStorage).inSingleton();
			bind(ClassNames::$SharingStorage).to(ClassNames::$MySqlSharingStorage).inSingleton();
			bind(ClassNames::$TwitterStorage).to(ClassNames::$MySqlTwitterStorage).inSingleton();

			bind(ClassNames::$Logger).to(ClassNames::$DumpLogger).inSingleton();
		}

		/**
		 * \Sharbat\@Provides(Mishiin\Router)
		 * \Sharbat\@Singleton
		 */
		public function provideRouter() {
			$routeProvider = new ApplicationRouteProvider();
			return $routeProvider.getRouter();
		}

		/**
		 * \Sharbat\@Provides(Mishiin\Request)
		 * \Sharbat\@Singleton
		 */
		public function provideRequest(){
			$requestProvider = new RequestProvider();
			return $requestProvider.get();
		}

		/**
		 * \Sharbat\@Provides(Mishiin\Response)
		 * \Sharbat\@Singleton
		 */
		public function provideResponse(){
			return new Response(array(), null);
		}

	}

}