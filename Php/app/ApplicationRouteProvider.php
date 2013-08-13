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
			builder = Router::builder();
			routeHome();
			routeAuth();
			routeUser();
			routeHomegame();
			routePlayer();
			routeCashgame();
			routeSharing();
		}

		public function getRouter(){
			return builder.build();
		}

		private function routeHome(){
			$controllerRouteParam = getControllerRouteParam('app\Home\HomeController');
			builder.createRoute(RouteFormats::home, $controllerRouteParam);
		}

		private function routeAuth(){
			$controllerClassName = 'app\Auth\AuthController';
			addRoute(RouteFormats::authLogin, $controllerClassName);
			addRoute(RouteFormats::authLogout, $controllerClassName);
		}

		private function routeUser(){
			routeUserDetails();
			routeUserEdit();
			routeUserAdd();
			routeChangePassword();
			routeForgotPassword();
			routeUserListing();
		}

		private function routeUserDetails(){
			$controllerClassName = 'app\User\Details\UserDetailsController';
			addUserRoute(RouteFormats::userDetails, $controllerClassName);
		}

		private function routeUserEdit(){
			$controllerClassName = 'app\User\Edit\UserEditController';
			addUserRoute(RouteFormats::userEdit, $controllerClassName);
		}

		private function routeUserAdd(){
			$controllerClassName = 'app\User\Add\UserAddController';
			addRoute(RouteFormats::userAdd, $controllerClassName);
			addRoute(RouteFormats::userAddConfirmation, $controllerClassName);
		}

		private function routeChangePassword(){
			$controllerClassName = 'app\User\ChangePassword\ChangePasswordController';
			addRoute(RouteFormats::changePassword, $controllerClassName);
			addRoute(RouteFormats::changePasswordConfirmation, $controllerClassName);
		}

		private function routeForgotPassword(){
			$controllerClassName = 'app\User\ForgotPassword\ForgotPasswordController';
			addRoute(RouteFormats::forgotPassword, $controllerClassName);
			addRoute(RouteFormats::forgotPasswordConfirmation, $controllerClassName);
		}

		private function routeUserListing(){
			$controllerClassName = 'app\User\Listing\UserListingController';
			addRoute(RouteFormats::userListing, $controllerClassName);
		}

		private function routeHomegame(){
			routeHomegameDetails();
			routeHomegameAdd();
			routeHomegameEdit();
			routeHomegameJoin();
			routeHomegameListing();
		}

		private function routeHomegameDetails(){
			$controllerClassName = 'app\Homegame\Details\HomegameDetailsController';
			addHomegameRoute(RouteFormats::homegameDetails, $controllerClassName);
		}

		private function routeHomegameEdit(){
			$controllerClassName = 'app\Homegame\Edit\HomegameEditController';
			addHomegameRoute(RouteFormats::homegameEdit, $controllerClassName);
		}

		private function routeHomegameJoin(){
			$controllerClassName = 'app\Homegame\Join\HomegameJoinController';
			addHomegameRoute(RouteFormats::homegameJoin, $controllerClassName);
			addHomegameRoute(RouteFormats::homegameJoinConfirmation, $controllerClassName);
		}

		private function routeHomegameAdd(){
			$controllerClassName = 'app\Homegame\Add\HomegameAddController';
			addRoute(RouteFormats::homegameAdd, $controllerClassName);
			addRoute(RouteFormats::homegameAddConfirmation, $controllerClassName);
		}

		private function routeHomegameListing(){
			$controllerClassName = 'app\Homegame\Listing\HomegameListingController';
			addRoute(RouteFormats::homegameListing, $controllerClassName);
		}

		private function routePlayer(){
			routePlayerIndex();
			routePlayerAdd();
			routePlayerDetails();
			routePlayerInvite();
		}

		private function routePlayerIndex(){
			$controllerClassName = 'app\Player\Listing\PlayerListingController';
			addHomegameRoute(RouteFormats::playerIndex, $controllerClassName);
		}

		private function routePlayerAdd(){
			$controllerClassName = 'app\Player\Add\PlayerAddController';
			addHomegameRoute(RouteFormats::playerAdd, $controllerClassName);
			addHomegameRoute(RouteFormats::playerAddConfirmation, $controllerClassName);
		}

		private function routePlayerDetails(){
			$controllerClassName = 'app\Player\Details\PlayerDetailsController';
			addPlayerRoute(RouteFormats::playerDetails, $controllerClassName);
			addPlayerRoute(RouteFormats::playerDelete, $controllerClassName);
		}

		private function routePlayerInvite(){
			$controllerClassName = 'app\Player\Invite\PlayerInviteController';
			addPlayerRoute(RouteFormats::playerInvite, $controllerClassName);
			addPlayerRoute(RouteFormats::playerInviteConfirmation, $controllerClassName);
		}

		private function routeCashgame(){
			routeCashgameIndex();
			routeCashgameDetails();
			routeCashgameEdit();
			routeCashgameAdd();
			routeCashgameChart();
			routeCashgameLeaderboard();
			routeCashgameListing();
			routeCashgameFacts();
			routeCashgameMatrix();
			routeCashgameAction();
			routeCashgameBuyin();
			routeCashgameReport();
			routeCashgameCashout();
			routeCashgameEnd();
			routeCashgameRunning();
			routeCashgameCheckpoint();
		}

		private function routeCashgameIndex(){
			$controllerClassName = 'app\Cashgame\Index\CashgameIndexController';
			addHomegameRoute(RouteFormats::cashgameIndex, $controllerClassName);
		}

		private function routeCashgameDetails(){
			$controllerClassName = 'app\Cashgame\Details\DetailsController';
			addCashgameRoute(RouteFormats::cashgameDetails, $controllerClassName);
			addCashgameRoute(RouteFormats::cashgameDetailsChartJson, $controllerClassName);
		}

		private function routeCashgameEdit(){
			$controllerClassName = 'app\Cashgame\Edit\CashgameEditController';
			addCashgameRoute(RouteFormats::cashgameEdit, $controllerClassName);
			addCashgameRoute(RouteFormats::cashgameDelete, $controllerClassName);
		}

		private function routeCashgameAdd(){
			$controllerClassName = 'app\Cashgame\Add\AddController';
			addHomegameRoute(RouteFormats::cashgameAdd, $controllerClassName);
		}

		private function routeCashgameChart(){
			$controllerClassName = 'app\Cashgame\Chart\ChartController';
			addCashgameSuiteRoute(RouteFormats::cashgameChartWithYear, $controllerClassName);
			addHomegameRoute(RouteFormats::cashgameChart, $controllerClassName);
			addCashgameSuiteRoute(RouteFormats::cashgameChartJsonWithYear, $controllerClassName);
			addHomegameRoute(RouteFormats::cashgameChartJson, $controllerClassName);
		}

		private function routeCashgameListing(){
			$controllerClassName = 'app\Cashgame\Listing\CashgameListingController';
			addCashgameSuiteRoute(RouteFormats::cashgameListingWithYear, $controllerClassName);
			addHomegameRoute(RouteFormats::cashgameListing, $controllerClassName);
		}

		private function routeCashgameFacts(){
			$controllerClassName = 'app\Cashgame\Facts\CashgameFactsController';
			addCashgameSuiteRoute(RouteFormats::cashgameFactsWithYear, $controllerClassName);
			addHomegameRoute(RouteFormats::cashgameFacts, $controllerClassName);
		}

		private function routeCashgameLeaderboard(){
			$controllerClassName = 'app\Cashgame\Leaderboard\LeaderboardController';
			addCashgameSuiteRoute(RouteFormats::cashgameLeaderboardWithYear, $controllerClassName);
			addHomegameRoute(RouteFormats::cashgameLeaderboard, $controllerClassName);
		}

		private function routeCashgameMatrix(){
			$controllerClassName = 'app\Cashgame\Matrix\MatrixController';
			addCashgameSuiteRoute(RouteFormats::cashgameMatrixWithYear, $controllerClassName);
			addHomegameRoute(RouteFormats::cashgameMatrix, $controllerClassName);
		}

		private function routeCashgameAction(){
			$controllerClassName = 'app\Cashgame\Action\ActionController';
			$params = getControllerRouteParam($controllerClassName);
			$pattern = sprintf(RouteFormats::cashgameAction, getRouteParameter(self::homegameParameter), getRouteParameter(self::cashgameParameter), getRouteParameter(self::playerParameter));
			builder.createRoute($pattern, $params)
				.where(self::homegameParameter)
				.matches(self::alphaNumericPattern)
				.where(self::cashgameParameter)
				.matches(self::datePattern)
				.where(self::playerParameter)
				.matches(self::alphaNumericWithEscapedUrlCharsPattern);

			$params = getControllerRouteParam($controllerClassName);
			$pattern = sprintf(RouteFormats::cashgameActionChartJson, getRouteParameter(self::homegameParameter), getRouteParameter(self::cashgameParameter), getRouteParameter(self::playerParameter));
			builder.createRoute($pattern, $params)
				.where(self::homegameParameter)
				.matches(self::alphaNumericPattern)
				.where(self::cashgameParameter)
				.matches(self::datePattern)
				.where(self::playerParameter)
				.matches(self::alphaNumericWithEscapedUrlCharsPattern);
		}

		private function routeCashgameBuyin(){
			$controllerClassName = 'app\Cashgame\Action\BuyinController';
			$params = getControllerRouteParam($controllerClassName);
			$pattern = sprintf(RouteFormats::cashgameBuyin, getRouteParameter(self::homegameParameter), getRouteParameter(self::playerParameter));
			builder.createRoute($pattern, $params)
				.where(self::homegameParameter)
				.matches(self::alphaNumericPattern)
				.where(self::playerParameter)
				.matches(self::alphaNumericWithEscapedUrlCharsPattern);
		}

		private function routeCashgameReport(){
			$controllerClassName = 'app\Cashgame\Action\ReportController';
			$params = getControllerRouteParam($controllerClassName);
			$pattern = sprintf(RouteFormats::cashgameReport, getRouteParameter(self::homegameParameter), getRouteParameter(self::playerParameter));
			builder.createRoute($pattern, $params)
				.where(self::homegameParameter)
				.matches(self::alphaNumericPattern)
				.where(self::playerParameter)
				.matches(self::alphaNumericWithEscapedUrlCharsPattern);
		}

		private function routeCashgameCashout(){
			$controllerClassName = 'app\Cashgame\Action\CashoutController';
			$params = getControllerRouteParam($controllerClassName);
			$pattern = sprintf(RouteFormats::cashgameCashout, getRouteParameter(self::homegameParameter), getRouteParameter(self::playerParameter));
			builder.createRoute($pattern, $params)
				.where(self::homegameParameter)
				.matches(self::alphaNumericPattern)
				.where(self::playerParameter)
				.matches(self::alphaNumericWithEscapedUrlCharsPattern);
		}

		private function routeCashgameEnd(){
			$controllerClassName = 'app\Cashgame\Action\EndGameController';
			addHomegameRoute(RouteFormats::cashgameEnd, $controllerClassName);
		}

		private function routeCashgameRunning(){
			$controllerClassName = 'app\Cashgame\Running\RunningController';
			addHomegameRoute(RouteFormats::runningCashgame, $controllerClassName);
		}

		private function routeCashgameCheckpoint(){
			$controllerClassName = 'app\Cashgame\Action\CheckpointController';
			$params = getControllerRouteParam($controllerClassName);
			$pattern = sprintf(RouteFormats::cashgameCheckpointDelete, getRouteParameter(self::homegameParameter), getRouteParameter(self::cashgameParameter), getRouteParameter(self::playerParameter), getRouteParameter(self::checkpointParameter));
			builder.createRoute($pattern, $params)
				.where(self::homegameParameter)
				.matches(self::alphaNumericPattern)
				.where(self::cashgameParameter)
				.matches(self::datePattern)
				.where(self::playerParameter)
				.matches(self::alphaNumericWithEscapedUrlCharsPattern)
				.where(self::checkpointParameter)
				.matches(self::numericPattern);
		}

		private function routeSharing(){
			routeSharingIndex();
			routeSharingTwitter();
		}

		private function routeSharingIndex(){
			$controllerClassName = 'app\Sharing\Index\SharingIndexController';
			addRoute(RouteFormats::sharingSettings, $controllerClassName);
		}

		private function routeSharingTwitter(){
			$controllerClassName = 'app\Sharing\Twitter\SharingTwitterController';
			addRoute(RouteFormats::twitterSettings, $controllerClassName);
			addRoute(RouteFormats::twitterStartShare, $controllerClassName);
			addRoute(RouteFormats::twitterStopShare, $controllerClassName);
			addRoute(RouteFormats::twitterCallback, $controllerClassName);
		}

		private function addRoute($format, $controllerClassName){
			$params = getControllerRouteParam($controllerClassName);
			builder.createRoute($format, $params);
		}

		private function addUserRoute($format, $controllerClassName){
			$params = getControllerRouteParam($controllerClassName);
			$pattern = sprintf($format, getRouteParameter(self::userParameter));
			builder.createRoute($pattern, $params)
				.where(self::userParameter)
				.matches(self::alphaNumericPattern);
		}

		private function addHomegameRoute($format, $controllerClassName){
			$params = getControllerRouteParam($controllerClassName);
			$pattern = sprintf($format, getRouteParameter(self::homegameParameter));
			builder.createRoute($pattern, $params)
				.where(self::homegameParameter)
				.matches(self::alphaNumericPattern);
		}

		private function addCashgameRoute($format, $controllerClassName){
			$params = getControllerRouteParam($controllerClassName);
			$pattern = sprintf($format, getRouteParameter(self::homegameParameter), getRouteParameter(self::cashgameParameter));
			builder.createRoute($pattern, $params)
				.where(self::homegameParameter)
				.matches(self::alphaNumericPattern)
				.where(self::cashgameParameter)
				.matches(self::datePattern);
		}

		private function addCashgameSuiteRoute($format, $controllerClassName){
			$params = getControllerRouteParam($controllerClassName);
			$pattern = sprintf($format, getRouteParameter(self::homegameParameter), getRouteParameter(self::yearParameter));
			builder.createRoute($pattern, $params)
				.where(self::homegameParameter)
				.matches(self::alphaNumericPattern)
				.where(self::yearParameter)
				.matches(self::numericPattern);
		}

		private function addPlayerRoute($format, $controllerClassName){
			$params = getControllerRouteParam($controllerClassName);
			$pattern = sprintf($format, getRouteParameter(self::homegameParameter), getRouteParameter(self::playerParameter));
			builder.createRoute($pattern, $params)
				.where(self::homegameParameter)
				.matches(self::alphaNumericPattern)
				.where(self::playerParameter)
				.matches(self::alphaNumericWithEscapedUrlCharsPattern);
		}

		private function getRouteParameter($str){
			return '<' . $str . '>';
		}

		private function getControllerRouteParam($controllerName){
			return array('controller' => $controllerName);
		}

	}

}