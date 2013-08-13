namespace app{

	use Mishiin\Router;

	class ApplicationRouteProvider {

		const numericPattern = '[0-9_]+';
		const alphaNumericPattern = '[a-zA-Z0-9_]+';
		const alphaNumericWithEscapedUrlCharsPattern = '[a-zA-Z0-9_%]+';
		const datePattern = '[0-9]{4}-[0-9]{2}-[0-9]{2}';

		const userParameter = 'user';
		const homegameParameter = 'homegame';
		const playerParameter = 'player';
		const cashgameParameter = 'cashgame';
		const yearParameter = 'year';
		const checkpointParameter = 'checkpoint';

		private $builder;

		function __construct() {
			$this->builder = Router::builder();
			$this->routeHome();
			$this->routeAuth();
			$this->routeUser();
			$this->routeHomegame();
			$this->routePlayer();
			$this->routeCashgame();
			$this->routeSharing();
		}

		public function getRouter(){
			return $this->builder->build();
		}

		private function routeHome(){
			$controllerRouteParam = $this->getControllerRouteParam('app\Home\HomeController');
			$this->builder->createRoute(RouteFormats::home, $controllerRouteParam);
		}

		private function routeAuth(){
			$controllerClassName = 'app\Auth\AuthController';
			$this->addRoute(RouteFormats::authLogin, $controllerClassName);
			$this->addRoute(RouteFormats::authLogout, $controllerClassName);
		}

		private function routeUser(){
			$this->routeUserDetails();
			$this->routeUserEdit();
			$this->routeUserAdd();
			$this->routeChangePassword();
			$this->routeForgotPassword();
			$this->routeUserListing();
		}

		private function routeUserDetails(){
			$controllerClassName = 'app\User\Details\UserDetailsController';
			$this->addUserRoute(RouteFormats::userDetails, $controllerClassName);
		}

		private function routeUserEdit(){
			$controllerClassName = 'app\User\Edit\UserEditController';
			$this->addUserRoute(RouteFormats::userEdit, $controllerClassName);
		}

		private function routeUserAdd(){
			$controllerClassName = 'app\User\Add\UserAddController';
			$this->addRoute(RouteFormats::userAdd, $controllerClassName);
			$this->addRoute(RouteFormats::userAddConfirmation, $controllerClassName);
		}

		private function routeChangePassword(){
			$controllerClassName = 'app\User\ChangePassword\ChangePasswordController';
			$this->addRoute(RouteFormats::changePassword, $controllerClassName);
			$this->addRoute(RouteFormats::changePasswordConfirmation, $controllerClassName);
		}

		private function routeForgotPassword(){
			$controllerClassName = 'app\User\ForgotPassword\ForgotPasswordController';
			$this->addRoute(RouteFormats::forgotPassword, $controllerClassName);
			$this->addRoute(RouteFormats::forgotPasswordConfirmation, $controllerClassName);
		}

		private function routeUserListing(){
			$controllerClassName = 'app\User\Listing\UserListingController';
			$this->addRoute(RouteFormats::userListing, $controllerClassName);
		}

		private function routeHomegame(){
			$this->routeHomegameDetails();
			$this->routeHomegameAdd();
			$this->routeHomegameEdit();
			$this->routeHomegameJoin();
			$this->routeHomegameListing();
		}

		private function routeHomegameDetails(){
			$controllerClassName = 'app\Homegame\Details\HomegameDetailsController';
			$this->addHomegameRoute(RouteFormats::homegameDetails, $controllerClassName);
		}

		private function routeHomegameEdit(){
			$controllerClassName = 'app\Homegame\Edit\HomegameEditController';
			$this->addHomegameRoute(RouteFormats::homegameEdit, $controllerClassName);
		}

		private function routeHomegameJoin(){
			$controllerClassName = 'app\Homegame\Join\HomegameJoinController';
			$this->addHomegameRoute(RouteFormats::homegameJoin, $controllerClassName);
			$this->addHomegameRoute(RouteFormats::homegameJoinConfirmation, $controllerClassName);
		}

		private function routeHomegameAdd(){
			$controllerClassName = 'app\Homegame\Add\HomegameAddController';
			$this->addRoute(RouteFormats::homegameAdd, $controllerClassName);
			$this->addRoute(RouteFormats::homegameAddConfirmation, $controllerClassName);
		}

		private function routeHomegameListing(){
			$controllerClassName = 'app\Homegame\Listing\HomegameListingController';
			$this->addRoute(RouteFormats::homegameListing, $controllerClassName);
		}

		private function routePlayer(){
			$this->routePlayerIndex();
			$this->routePlayerAdd();
			$this->routePlayerDetails();
			$this->routePlayerInvite();
		}

		private function routePlayerIndex(){
			$controllerClassName = 'app\Player\Listing\PlayerListingController';
			$this->addHomegameRoute(RouteFormats::playerIndex, $controllerClassName);
		}

		private function routePlayerAdd(){
			$controllerClassName = 'app\Player\Add\PlayerAddController';
			$this->addHomegameRoute(RouteFormats::playerAdd, $controllerClassName);
			$this->addHomegameRoute(RouteFormats::playerAddConfirmation, $controllerClassName);
		}

		private function routePlayerDetails(){
			$controllerClassName = 'app\Player\Details\PlayerDetailsController';
			$this->addPlayerRoute(RouteFormats::playerDetails, $controllerClassName);
			$this->addPlayerRoute(RouteFormats::playerDelete, $controllerClassName);
		}

		private function routePlayerInvite(){
			$controllerClassName = 'app\Player\Invite\PlayerInviteController';
			$this->addPlayerRoute(RouteFormats::playerInvite, $controllerClassName);
			$this->addPlayerRoute(RouteFormats::playerInviteConfirmation, $controllerClassName);
		}

		private function routeCashgame(){
			$this->routeCashgameIndex();
			$this->routeCashgameDetails();
			$this->routeCashgameEdit();
			$this->routeCashgameAdd();
			$this->routeCashgameChart();
			$this->routeCashgameLeaderboard();
			$this->routeCashgameListing();
			$this->routeCashgameFacts();
			$this->routeCashgameMatrix();
			$this->routeCashgameAction();
			$this->routeCashgameBuyin();
			$this->routeCashgameReport();
			$this->routeCashgameCashout();
			$this->routeCashgameEnd();
			$this->routeCashgameRunning();
			$this->routeCashgameCheckpoint();
		}

		private function routeCashgameIndex(){
			$controllerClassName = 'app\Cashgame\Index\CashgameIndexController';
			$this->addHomegameRoute(RouteFormats::cashgameIndex, $controllerClassName);
		}

		private function routeCashgameDetails(){
			$controllerClassName = 'app\Cashgame\Details\DetailsController';
			$this->addCashgameRoute(RouteFormats::cashgameDetails, $controllerClassName);
			$this->addCashgameRoute(RouteFormats::cashgameDetailsChartJson, $controllerClassName);
		}

		private function routeCashgameEdit(){
			$controllerClassName = 'app\Cashgame\Edit\CashgameEditController';
			$this->addCashgameRoute(RouteFormats::cashgameEdit, $controllerClassName);
			$this->addCashgameRoute(RouteFormats::cashgameDelete, $controllerClassName);
		}

		private function routeCashgameAdd(){
			$controllerClassName = 'app\Cashgame\Add\AddController';
			$this->addHomegameRoute(RouteFormats::cashgameAdd, $controllerClassName);
		}

		private function routeCashgameChart(){
			$controllerClassName = 'app\Cashgame\Chart\ChartController';
			$this->addCashgameSuiteRoute(RouteFormats::cashgameChartWithYear, $controllerClassName);
			$this->addHomegameRoute(RouteFormats::cashgameChart, $controllerClassName);
			$this->addCashgameSuiteRoute(RouteFormats::cashgameChartJsonWithYear, $controllerClassName);
			$this->addHomegameRoute(RouteFormats::cashgameChartJson, $controllerClassName);
		}

		private function routeCashgameListing(){
			$controllerClassName = 'app\Cashgame\Listing\CashgameListingController';
			$this->addCashgameSuiteRoute(RouteFormats::cashgameListingWithYear, $controllerClassName);
			$this->addHomegameRoute(RouteFormats::cashgameListing, $controllerClassName);
		}

		private function routeCashgameFacts(){
			$controllerClassName = 'app\Cashgame\Facts\CashgameFactsController';
			$this->addCashgameSuiteRoute(RouteFormats::cashgameFactsWithYear, $controllerClassName);
			$this->addHomegameRoute(RouteFormats::cashgameFacts, $controllerClassName);
		}

		private function routeCashgameLeaderboard(){
			$controllerClassName = 'app\Cashgame\Leaderboard\LeaderboardController';
			$this->addCashgameSuiteRoute(RouteFormats::cashgameLeaderboardWithYear, $controllerClassName);
			$this->addHomegameRoute(RouteFormats::cashgameLeaderboard, $controllerClassName);
		}

		private function routeCashgameMatrix(){
			$controllerClassName = 'app\Cashgame\Matrix\MatrixController';
			$this->addCashgameSuiteRoute(RouteFormats::cashgameMatrixWithYear, $controllerClassName);
			$this->addHomegameRoute(RouteFormats::cashgameMatrix, $controllerClassName);
		}

		private function routeCashgameAction(){
			$controllerClassName = 'app\Cashgame\Action\ActionController';
			$params = $this->getControllerRouteParam($controllerClassName);
			$pattern = sprintf(RouteFormats::cashgameAction, $this->getRouteParameter(self::homegameParameter), $this->getRouteParameter(self::cashgameParameter), $this->getRouteParameter(self::playerParameter));
			$this->builder->createRoute($pattern, $params)
				->where(self::homegameParameter)
				->matches(self::alphaNumericPattern)
				->where(self::cashgameParameter)
				->matches(self::datePattern)
				->where(self::playerParameter)
				->matches(self::alphaNumericWithEscapedUrlCharsPattern);

			$params = $this->getControllerRouteParam($controllerClassName);
			$pattern = sprintf(RouteFormats::cashgameActionChartJson, $this->getRouteParameter(self::homegameParameter), $this->getRouteParameter(self::cashgameParameter), $this->getRouteParameter(self::playerParameter));
			$this->builder->createRoute($pattern, $params)
				->where(self::homegameParameter)
				->matches(self::alphaNumericPattern)
				->where(self::cashgameParameter)
				->matches(self::datePattern)
				->where(self::playerParameter)
				->matches(self::alphaNumericWithEscapedUrlCharsPattern);
		}

		private function routeCashgameBuyin(){
			$controllerClassName = 'app\Cashgame\Action\BuyinController';
			$params = $this->getControllerRouteParam($controllerClassName);
			$pattern = sprintf(RouteFormats::cashgameBuyin, $this->getRouteParameter(self::homegameParameter), $this->getRouteParameter(self::playerParameter));
			$this->builder->createRoute($pattern, $params)
				->where(self::homegameParameter)
				->matches(self::alphaNumericPattern)
				->where(self::playerParameter)
				->matches(self::alphaNumericWithEscapedUrlCharsPattern);
		}

		private function routeCashgameReport(){
			$controllerClassName = 'app\Cashgame\Action\ReportController';
			$params = $this->getControllerRouteParam($controllerClassName);
			$pattern = sprintf(RouteFormats::cashgameReport, $this->getRouteParameter(self::homegameParameter), $this->getRouteParameter(self::playerParameter));
			$this->builder->createRoute($pattern, $params)
				->where(self::homegameParameter)
				->matches(self::alphaNumericPattern)
				->where(self::playerParameter)
				->matches(self::alphaNumericWithEscapedUrlCharsPattern);
		}

		private function routeCashgameCashout(){
			$controllerClassName = 'app\Cashgame\Action\CashoutController';
			$params = $this->getControllerRouteParam($controllerClassName);
			$pattern = sprintf(RouteFormats::cashgameCashout, $this->getRouteParameter(self::homegameParameter), $this->getRouteParameter(self::playerParameter));
			$this->builder->createRoute($pattern, $params)
				->where(self::homegameParameter)
				->matches(self::alphaNumericPattern)
				->where(self::playerParameter)
				->matches(self::alphaNumericWithEscapedUrlCharsPattern);
		}

		private function routeCashgameEnd(){
			$controllerClassName = 'app\Cashgame\Action\EndGameController';
			$this->addHomegameRoute(RouteFormats::cashgameEnd, $controllerClassName);
		}

		private function routeCashgameRunning(){
			$controllerClassName = 'app\Cashgame\Running\RunningController';
			$this->addHomegameRoute(RouteFormats::runningCashgame, $controllerClassName);
		}

		private function routeCashgameCheckpoint(){
			$controllerClassName = 'app\Cashgame\Action\CheckpointController';
			$params = $this->getControllerRouteParam($controllerClassName);
			$pattern = sprintf(RouteFormats::cashgameCheckpointDelete, $this->getRouteParameter(self::homegameParameter), $this->getRouteParameter(self::cashgameParameter), $this->getRouteParameter(self::playerParameter), $this->getRouteParameter(self::checkpointParameter));
			$this->builder->createRoute($pattern, $params)
				->where(self::homegameParameter)
				->matches(self::alphaNumericPattern)
				->where(self::cashgameParameter)
				->matches(self::datePattern)
				->where(self::playerParameter)
				->matches(self::alphaNumericWithEscapedUrlCharsPattern)
				->where(self::checkpointParameter)
				->matches(self::numericPattern);
		}

		private function routeSharing(){
			$this->routeSharingIndex();
			$this->routeSharingTwitter();
		}

		private function routeSharingIndex(){
			$controllerClassName = 'app\Sharing\Index\SharingIndexController';
			$this->addRoute(RouteFormats::sharingSettings, $controllerClassName);
		}

		private function routeSharingTwitter(){
			$controllerClassName = 'app\Sharing\Twitter\SharingTwitterController';
			$this->addRoute(RouteFormats::twitterSettings, $controllerClassName);
			$this->addRoute(RouteFormats::twitterStartShare, $controllerClassName);
			$this->addRoute(RouteFormats::twitterStopShare, $controllerClassName);
			$this->addRoute(RouteFormats::twitterCallback, $controllerClassName);
		}

		private function addRoute($format, $controllerClassName){
			$params = $this->getControllerRouteParam($controllerClassName);
			$this->builder->createRoute($format, $params);
		}

		private function addUserRoute($format, $controllerClassName){
			$params = $this->getControllerRouteParam($controllerClassName);
			$pattern = sprintf($format, $this->getRouteParameter(self::userParameter));
			$this->builder->createRoute($pattern, $params)
				->where(self::userParameter)
				->matches(self::alphaNumericPattern);
		}

		private function addHomegameRoute($format, $controllerClassName){
			$params = $this->getControllerRouteParam($controllerClassName);
			$pattern = sprintf($format, $this->getRouteParameter(self::homegameParameter));
			$this->builder->createRoute($pattern, $params)
				->where(self::homegameParameter)
				->matches(self::alphaNumericPattern);
		}

		private function addCashgameRoute($format, $controllerClassName){
			$params = $this->getControllerRouteParam($controllerClassName);
			$pattern = sprintf($format, $this->getRouteParameter(self::homegameParameter), $this->getRouteParameter(self::cashgameParameter));
			$this->builder->createRoute($pattern, $params)
				->where(self::homegameParameter)
				->matches(self::alphaNumericPattern)
				->where(self::cashgameParameter)
				->matches(self::datePattern);
		}

		private function addCashgameSuiteRoute($format, $controllerClassName){
			$params = $this->getControllerRouteParam($controllerClassName);
			$pattern = sprintf($format, $this->getRouteParameter(self::homegameParameter), $this->getRouteParameter(self::yearParameter));
			$this->builder->createRoute($pattern, $params)
				->where(self::homegameParameter)
				->matches(self::alphaNumericPattern)
				->where(self::yearParameter)
				->matches(self::numericPattern);
		}

		private function addPlayerRoute($format, $controllerClassName){
			$params = $this->getControllerRouteParam($controllerClassName);
			$pattern = sprintf($format, $this->getRouteParameter(self::homegameParameter), $this->getRouteParameter(self::playerParameter));
			$this->builder->createRoute($pattern, $params)
				->where(self::homegameParameter)
				->matches(self::alphaNumericPattern)
				->where(self::playerParameter)
				->matches(self::alphaNumericWithEscapedUrlCharsPattern);
		}

		private function getRouteParameter($str){
			return '<' . $str . '>';
		}

		private function getControllerRouteParam($controllerName){
			return array('controller' => $controllerName);
		}

	}

}