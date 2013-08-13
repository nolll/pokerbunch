namespace app{

	use core\ClassNames;
	use Mishiin\RequestProvider;
	use Mishiin\Response;

	class ApplicationModule extends MishiinModule {

		public function configure() {
			parent::configure();
			parent::bindTemplateEngine();
			parent::bindParamParser('app\AppParamParser');

			$this->bind(ClassNames::$MishiinConfigFactory)->to(ClassNames::$AppConfigFactory)->inSingleton();
			$this->bind(ClassNames::$UserContext)->to(ClassNames::$UserContextImpl)->inSingleton();
			$this->bind(ClassNames::$WebContext)->to(ClassNames::$WebContextImpl)->inSingleton();
			$this->bind(ClassNames::$Timer)->to(ClassNames::$TimerImpl)->inSingleton();

			$this->bind(ClassNames::$SlugGenerator)->to(ClassNames::$SlugGeneratorImpl)->inSingleton();
			$this->bind(ClassNames::$HomegameValidatorFactory)->to(ClassNames::$HomegameValidatorFactoryImpl)->inSingleton();

			$this->bind(ClassNames::$CashgameValidatorFactory)->to(ClassNames::$CashgameValidatorFactoryImpl)->inSingleton();

			$this->bind(ClassNames::$PlayerValidatorFactory)->to(ClassNames::$PlayerValidatorFactoryImpl)->inSingleton();
			$this->bind(ClassNames::$InvitationSender)->to(ClassNames::$InvitationSenderImpl)->inSingleton();
			$this->bind(ClassNames::$InvitationCodeCreator)->to(ClassNames::$InvitationCodeCreatorImpl)->inSingleton();

			$this->bind(ClassNames::$Encryption)->to(ClassNames::$EncryptionImpl)->inSingleton();
			$this->bind(ClassNames::$UserFactory)->to(ClassNames::$UserFactoryImpl)->inSingleton();
			$this->bind(ClassNames::$UserValidatorFactory)->to(ClassNames::$UserValidatorFactoryImpl)->inSingleton();
			$this->bind(ClassNames::$RegistrationConfirmationSender)->to(ClassNames::$RegistrationConfirmationSenderImpl)->inSingleton();
			$this->bind(ClassNames::$PasswordSender)->to(ClassNames::$PasswordSenderImpl)->inSingleton();

			$this->bind(ClassNames::$CashgameFactory)->to(ClassNames::$CashgameFactoryImpl)->inSingleton();
			$this->bind(ClassNames::$CashgameSuiteFactory)->to(ClassNames::$CashgameSuiteFactoryImpl)->inSingleton();
			$this->bind(ClassNames::$CashgameResultFactory)->to(ClassNames::$CashgameResultFactoryImpl)->inSingleton();
			$this->bind(ClassNames::$CashgameTotalResultFactory)->to(ClassNames::$CashgameTotalResultFactoryImpl)->inSingleton();

			$this->bind(ClassNames::$HomegameRepository)->to(ClassNames::$HomegameRepositoryImpl)->inSingleton();
			$this->bind(ClassNames::$CashgameRepository)->to(ClassNames::$CashgameRepositoryImpl)->inSingleton();
			$this->bind(ClassNames::$PlayerRepository)->to(ClassNames::$PlayerRepositoryImpl)->inSingleton();
			$this->bind(ClassNames::$PlayerFactory)->to(ClassNames::$PlayerFactoryImpl)->inSingleton();

			$this->bind(ClassNames::$SocialServiceFactory)->to(ClassNames::$SocialServiceFactoryImpl)->inSingleton();
			$this->bind(ClassNames::$TwitterService)->to(ClassNames::$TwitterServiceImpl)->inSingleton();
			$this->bind(ClassNames::$ResultSharer)->to(ClassNames::$ResultSharerImpl)->inSingleton();
			$this->bind(ClassNames::$MessageSenderFactory)->to(ClassNames::$MessageSenderFactoryImpl)->inSingleton();
			$this->bind(ClassNames::$AvatarService)->to(ClassNames::$GravatarService)->inSingleton();

			$this->bind(ClassNames::$MatrixModelFactory)->to(ClassNames::$MatrixModelFactoryImpl)->inSingleton();

			$this->bind(ClassNames::$StorageProvider)->to(ClassNames::$MySqlPdo)->inSingleton();
			$this->bind(ClassNames::$UserStorage)->to(ClassNames::$MySqlUserStorage)->inSingleton();
			$this->bind(ClassNames::$HomegameStorage)->to(ClassNames::$MySqlHomegameStorage)->inSingleton();
			$this->bind(ClassNames::$PlayerStorage)->to(ClassNames::$MySqlPlayerStorage)->inSingleton();
			$this->bind(ClassNames::$CashgameStorage)->to(ClassNames::$MySqlCashgameStorage)->inSingleton();
			$this->bind(ClassNames::$SharingStorage)->to(ClassNames::$MySqlSharingStorage)->inSingleton();
			$this->bind(ClassNames::$TwitterStorage)->to(ClassNames::$MySqlTwitterStorage)->inSingleton();

			$this->bind(ClassNames::$Logger)->to(ClassNames::$DumpLogger)->inSingleton();
		}

		/**
		 * \Sharbat\@Provides(Mishiin\Router)
		 * \Sharbat\@Singleton
		 */
		public function provideRouter() {
			$routeProvider = new ApplicationRouteProvider();
			return $routeProvider->getRouter();
		}

		/**
		 * \Sharbat\@Provides(Mishiin\Request)
		 * \Sharbat\@Singleton
		 */
		public function provideRequest(){
			$requestProvider = new RequestProvider();
			return $requestProvider->get();
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