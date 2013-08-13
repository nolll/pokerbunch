namespace tests\CoreTests{

	use DateTime;
	use app\Urls\HomegameJoinConfirmationUrlModel;
	use entities\Cashgame;
	use entities\Homegame;
	use Domain\Classes\User;
	use entities\Player;
	use tests\SharbatUnitTestCase;
	use app\Urls\HomeUrlModel;
	use app\Urls\CashgameCashoutUrlModel;
	use app\Urls\CashgameReportUrlModel;
	use app\Urls\CashgameBuyinUrlModel;
	use app\Urls\UserListingUrlModel;
	use app\Urls\UserEditUrlModel;
	use app\Urls\UserDetailsUrlModel;
	use app\Urls\UserAddUrlModel;
	use app\Urls\UserAddConfirmationUrlModel;
	use app\Urls\TwitterStopShareUrlModel;
	use app\Urls\TwitterStartShareUrlModel;
	use app\Urls\TwitterSettingsUrlModel;
	use app\Urls\SharingSettingsUrlModel;
	use app\Urls\PlayerInviteUrlModel;
	use app\Urls\PlayerIndexUrlModel;
	use app\Urls\PlayerDetailsUrlModel;
	use app\Urls\PlayerDeleteUrlModel;
	use app\Urls\PlayerAddConfirmationUrlModel;
	use app\Urls\PlayerAddUrlModel;
	use app\Urls\HomegameListingUrlModel;
	use app\Urls\HomegameJoinUrlModel;
	use app\Urls\HomegameEditUrlModel;
	use app\Urls\HomegameDetailsUrlModel;
	use app\Urls\HomegameAddConfirmationUrlModel;
	use app\Urls\HomegameAddUrlModel;
	use app\Urls\ForgotPasswordConfirmationUrlModel;
	use app\Urls\ForgotPasswordUrlModel;
	use app\Urls\ChangePasswordUrlModel;
	use app\Urls\ChangePasswordConfirmationUrlModel;
	use app\Urls\CashgameRemoveResultUrlModel;
	use app\Urls\CashgameListingUrlModel;
	use app\Urls\CashgameMatrixUrlModel;
	use app\Urls\CashgameLeaderboardUrlModel;
	use app\Urls\CashgameIndexUrlModel;
	use app\Urls\CashgameDeleteUrlModel;
	use app\Urls\AuthLogoutUrlModel;
	use app\Urls\CashgameAddUrlModel;
	use app\Urls\CashgameChartUrlModel;
	use app\Urls\AuthLoginUrlModel;
	use app\Urls\CashgameDetailsUrlModel;
	use app\Urls\CashgameActionUrlModel;
	use app\Urls\CashgamePublishUrlModel;
	use app\Urls\CashgameUnpublishUrlModel;
	use app\Urls\CashgameEditUrlModel;
	use tests\TestHelper;

	class UrlTests extends SharbatUnitTestCase {

		function test_HomeUrl(){
			$sut = new HomeUrlModel();

			assertIdentical("/", $sut.url);
		}

		function test_AuthLoginUrl(){
			$sut = new AuthLoginUrlModel();

			assertIdentical("/-/auth/login", $sut.url);
		}

		function test_AuthLogoutUrl(){
			$sut = new AuthLogoutUrlModel();

			assertIdentical("/-/auth/logout", $sut.url);
		}

		function test_CashgameAddUrl(){
			$homegame = getHomegame();

			$sut = new CashgameAddUrlModel($homegame);

			assertIdentical("/abc/cashgame/add", $sut.url);
		}

		function test_CashgameChartUrl_WithYear(){
			$homegame = getHomegame();
			$year = 2010;

			$sut = new CashgameChartUrlModel($homegame, $year);

			assertIdentical("/abc/cashgame/chart/2010", $sut.url);
		}

		function test_CashgameChartUrl_WithoutYear(){
			$homegame = getHomegame();
			$year = null;

			$sut = new CashgameChartUrlModel($homegame, $year);

			assertIdentical("/abc/cashgame/chart", $sut.url);
		}

		function test_CashgameDeleteUrl(){
			$homegame = getHomegame();
			$cashgame = new Cashgame();
			$cashgame.setStartTime(new DateTime('2010-01-01'));

			$sut = new CashgameDeleteUrlModel($homegame, $cashgame);

			assertIdentical("/abc/cashgame/delete/2010-01-01", $sut.url);
		}

		function test_CashgameDetailsUrl(){
			$homegame = getHomegame();
			$cashgame = new Cashgame();
			$cashgame.setStartTime(new DateTime('2010-01-01'));

			$sut = new CashgameDetailsUrlModel($homegame, $cashgame);

			assertIdentical("/abc/cashgame/details/2010-01-01", $sut.url);
		}

		function test_CashgameEditUrlModel_ReturnsCorrectUrl(){
			$homegame = getHomegame();
			$cashgame = new Cashgame();
			$cashgame.setStartTime(new DateTime('2010-01-01'));

			$sut = new CashgameEditUrlModel($homegame, $cashgame);

			assertIdentical("/abc/cashgame/edit/2010-01-01", $sut.url);
		}

		function test_CashgameIndexUrl(){
			$homegame = getHomegame();

			$sut = new CashgameIndexUrlModel($homegame);

			assertIdentical("/abc/cashgame/index", $sut.url);
		}

		function test_CashgameLeaderboardUrl_WithYear(){
			$homegame = getHomegame();
			$year = 2010;

			$sut = new CashgameLeaderboardUrlModel($homegame, $year);

			assertIdentical("/abc/cashgame/leaderboard/2010", $sut.url);
		}

		function test_CashgameLeaderboardUrl_WithoutYear(){
			$homegame = getHomegame();
			$year = null;

			$sut = new CashgameLeaderboardUrlModel($homegame, $year);

			assertIdentical("/abc/cashgame/leaderboard", $sut.url);
		}

		function test_CashgameMatrixUrl_WithYear(){
			$homegame = getHomegame();
			$year = 2010;

			$sut = new CashgameMatrixUrlModel($homegame, $year);

			assertIdentical("/abc/cashgame/matrix/2010", $sut.url);
		}

		function test_CashgameMatrixUrl_WithoutYear(){
			$homegame = getHomegame();
			$year = null;

			$sut = new CashgameMatrixUrlModel($homegame, $year);

			assertIdentical("/abc/cashgame/matrix", $sut.url);
		}

		function test_CashgameListingUrl_WithYear(){
			$homegame = getHomegame();
			$year = 2010;

			$sut = new CashgameListingUrlModel($homegame, $year);

			assertIdentical("/abc/cashgame/listing/2010", $sut.url);
		}

		function test_CashgameListingUrl_WithoutYear(){
			$homegame = getHomegame();
			$year = null;

			$sut = new CashgameListingUrlModel($homegame, $year);

			assertIdentical("/abc/cashgame/listing", $sut.url);
		}

		function test_CashgameActionUrlModel_ReturnsCorrectUrl(){
			$homegame = getHomegame();
			$cashgame = new Cashgame();
			$cashgame.setStartTime(new DateTime('2010-01-01'));
			$player = new Player();
			$player.setDisplayName('a');

			$sut = new CashgameActionUrlModel($homegame, $cashgame, $player);

			assertIdentical("/abc/cashgame/action/2010-01-01/a", $sut.url);
		}

		function test_CashgameBuyinUrlModel_ReturnsCorrectUrl(){
			$homegame = getHomegame();
			$player = new Player();
			$player.setDisplayName('a');

			$sut = new CashgameBuyinUrlModel($homegame, $player);

			assertIdentical("/abc/cashgame/buyin/a", $sut.url);
		}

		function test_CashgameReportUrlModel_ReturnsCorrectUrl(){
			$homegame = getHomegame();
			$player = new Player();
			$player.setDisplayName('a');

			$sut = new CashgameReportUrlModel($homegame, $player);

			assertIdentical("/abc/cashgame/report/a", $sut.url);
		}

		function test_CashgameCashoutUrlModel_ReturnsCorrectUrl(){
			$homegame = getHomegame();
			$player = new Player();
			$player.setDisplayName('a');

			$sut = new CashgameCashoutUrlModel($homegame, $player);

			assertIdentical("/abc/cashgame/cashout/a", $sut.url);
		}

		function test_CashgamePublishUrlModel_ReturnsCorrectUrl(){
			$homegame = getHomegame();
			$cashgame = new Cashgame();
			$cashgame.setStartTime(new DateTime('2010-01-01'));

			$sut = new CashgamePublishUrlModel($homegame, $cashgame);

			assertIdentical("/abc/cashgame/publish/2010-01-01", $sut.url);
		}

		function test_CashgameRemoveResultUrl(){
			$homegame = getHomegame();
			$cashgame = new Cashgame();
			$cashgame.setStartTime(new DateTime('2010-01-01'));

			$sut = new CashgameRemoveResultUrlModel($homegame, $cashgame);

			assertIdentical("/abc/cashgame/removeresult/2010-01-01", $sut.url);
		}

		function test_CashgameUnpublishUrlModel_ReturnsCorrectUrl(){
			$homegame = getHomegame();
			$cashgame = new Cashgame();
			$cashgame.setStartTime(new DateTime('2010-01-01'));

			$sut = new CashgameUnpublishUrlModel($homegame, $cashgame);

			assertIdentical("/abc/cashgame/unpublish/2010-01-01", $sut.url);
		}

		function test_ChangePasswordConfirmationUrl(){
			$sut = new ChangePasswordConfirmationUrlModel();

			assertIdentical("/-/password/changed", $sut.url);
		}

		function test_ChangePasswordFormUrl(){
			$sut = new ChangePasswordUrlModel();

			assertIdentical("/-/password/change", $sut.url);
		}

		function test_ForgotPasswordConfirmationUrl(){
			$sut = new ForgotPasswordConfirmationUrlModel();

			assertIdentical("/-/password/sent", $sut.url);
		}

		function test_ForgotPasswordFormUrl(){
			$sut = new ForgotPasswordUrlModel();

			assertIdentical("/-/password/forgot", $sut.url);
		}

		function test_HomegameAddUrl(){
			$sut = new HomegameAddUrlModel();

			assertIdentical("/-/game/add", $sut.url);
		}

		function test_HomegameAddConfirmationUrl(){
			$sut = new HomegameAddConfirmationUrlModel();

			assertIdentical("/-/game/created", $sut.url);
		}

		function test_HomegameDetailsUrl(){
			$homegame = getHomegame();

			$sut = new HomegameDetailsUrlModel($homegame);

			assertIdentical("/abc/game/details", $sut.url);
		}

		function test_HomegameEditUrl(){
			$homegame = getHomegame();

			$sut = new HomegameEditUrlModel($homegame);

			assertIdentical("/abc/game/edit", $sut.url);
		}

		function test_HomegameJoinUrl(){
			$homegame = getHomegame();

			$sut = new HomegameJoinUrlModel($homegame);

			assertIdentical("/abc/game/join", $sut.url);
		}

		function test_HomegameJoinConfirmationUrl(){
			$homegame = getHomegame();

			$sut = new HomegameJoinConfirmationUrlModel($homegame);

			assertIdentical("/abc/game/joined", $sut.url);
		}

		function test_HomegameListingUrl(){
			$sut = new HomegameListingUrlModel();

			assertIdentical("/-/game/listing", $sut.url);
		}

		function test_PlayerAddUrl(){
			$homegame = getHomegame();

			$sut = new PlayerAddUrlModel($homegame);

			assertIdentical("/abc/player/add", $sut.url);
		}

		function test_PlayerAddConfirmationUrl(){
			$homegame = getHomegame();

			$sut = new PlayerAddConfirmationUrlModel($homegame);

			assertIdentical("/abc/player/created", $sut.url);
		}

		function test_PlayerDeleteUrl(){
			$homegame = getHomegame();
			$player = new Player();
			$player.setDisplayName('a');

			$sut = new PlayerDeleteUrlModel($homegame, $player);

			assertIdentical("/abc/player/delete/a", $sut.url);
		}

		function test_PlayerDetailsUrl(){
			$homegame = getHomegame();
			$player = new Player();
			$player.setDisplayName('a');

			$sut = new PlayerDetailsUrlModel($homegame, $player);

			assertIdentical("/abc/player/details/a", $sut.url);
		}

		function test_PlayerIndexUrl(){
			$homegame = getHomegame();

			$sut = new PlayerIndexUrlModel($homegame);

			assertIdentical("/abc/player/index", $sut.url);
		}

		function test_PlayerInviteUrl(){
			$homegame = getHomegame();
			$player = new Player();
			$player.setDisplayName('a');

			$sut = new PlayerInviteUrlModel($homegame, $player);

			assertIdentical("/abc/player/invite/a", $sut.url);
		}

		function test_SharingSettingsUrl(){
			$sut = new SharingSettingsUrlModel();

			assertIdentical("/-/sharing", $sut.url);
		}

		function test_TwitterSettingsUrl(){
			$sut = new TwitterSettingsUrlModel();

			assertIdentical("/-/sharing/twitter", $sut.url);
		}

		function test_TwitterStartShareUrl(){
			$sut = new TwitterStartShareUrlModel();

			assertIdentical("/-/sharing/twitterstart", $sut.url);
		}

		function test_TwitterStopShareUrl(){
			$sut = new TwitterStopShareUrlModel();

			assertIdentical("/-/sharing/twitterstop", $sut.url);
		}

		function test_UserAddConfirmationUrl(){
			$sut = new UserAddConfirmationUrlModel();

			assertIdentical("/-/user/created", $sut.url);
		}

		function test_UserAddFormUrl(){
			$sut = new UserAddUrlModel();

			assertIdentical("/-/user/add", $sut.url);
		}

		function test_UserDetailsUrl(){
			$user = new User();
			$user.setUserName('a');

			$sut = new UserDetailsUrlModel($user);

			assertIdentical("/-/user/details/a", $sut.url);
		}

		function test_UserEditUrl(){
			$user = new User();
			$user.setUserName('a');

			$sut = new UserEditUrlModel($user);

			assertIdentical("/-/user/edit/a", $sut.url);
		}

		function test_UserListingUrl(){
			$sut = new UserListingUrlModel();

			assertIdentical("/-/user/listing", $sut.url);
		}

		private function getHomegame(){
			$homegame = new Homegame();
			$homegame.setSlug('abc');
			return $homegame;
		}

	}

}