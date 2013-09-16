namespace Web.Routing{

	public class RouteFormats{

        public const string Home = "/";

        public const string AuthLogin = "/-/auth/login";
        public const string AuthLogout = "/-/auth/logout";

        public const string CashgameAdd = "/{0}/cashgame/add";
        //const cashgameAddResult = '/%1$s/cashgame/addresult/%2$s';
        public const string CashgameChart = "/{0}/cashgame/chart";
        public const string CashgameChartWithYear = "/{0}/cashgame/chart/{1}";
        public const string CashgameChartJson = "/{0}/cashgame/chartjson";
        public const string CashgameChartJsonWithYear = "/{0}/cashgame/chartjson/{1}";
        public const string CashgameDelete = "/{0}/cashgame/delete/{1}";
        public const string CashgameDetails = "/{0}/cashgame/details/{1}";
        public const string CashgameDetailsChartJson = "/{0}/cashgame/detailschartjson/{1}";
        public const string CashgameEdit = "/{0}/cashgame/edit/{1}";
        public const string CashgameEnd = "/{0}/cashgame/end";
        public const string CashgameIndex = "/{0}/cashgame/index";
        public const string CashgameLeaderboard = "/{0}/cashgame/leaderboard";
        public const string CashgameLeaderboardWithYear = "/{0}/cashgame/leaderboard/{1}";
        public const string CashgameMatrix = "/{0}/cashgame/matrix";
        public const string CashgameMatrixWithYear = "/{0}/cashgame/matrix/{1}";
        public const string CashgameListing = "/{0}/cashgame/listing";
        public const string CashgameListingWithYear = "/{0}/cashgame/listing/{1}";
        public const string CashgameFacts = "/{0}/cashgame/facts";
        public const string CashgameFactsWithYear = "/{0}/cashgame/facts/{1}";
        public const string CashgameAction = "/{0}/cashgame/action/{1}/{2}";
        public const string CashgameActionChartJson = "/{0}/cashgame/actionchartjson/{1}/{2}";
        public const string CashgameBuyin = "/{0}/cashgame/buyin/{1}";
        public const string CashgameReport = "/{0}/cashgame/report/{1}";
        public const string CashgameCashout = "/{0}/cashgame/cashout/{1}";
        public const string CashgamePublish = "/{0}/cashgame/publish/{1}";
        //const cashgameRemoveResult = '/%1$s/cashgame/removeresult/%2$s';
        public const string CashgameUnpublish = "/{0}/cashgame/unpublish/{1}";
        public const string RunningCashgame = "/{0}/cashgame/running";
        public const string CashgameCheckpointDelete = "/{0}/cashgame/deletecheckpoint/{1}/{2}/{3}";

        public const string ChangePassword = "/-/password/change";
        //const changePasswordConfirmation = '/-/password/changed';

        public const string ForgotPassword = "/-/password/forgot";
        //const forgotPasswordConfirmation = '/-/password/sent';

        public const string HomegameAdd = "/-/homegame/add";
        public const string HomegameAddConfirmation = "/-/homegame/created";
        public const string HomegameDetails = "/{0}/homegame/details";
        public const string HomegameEdit = "/{0}/homegame/edit";
        //const homegameJoin = '/%1$s/homegame/join';
        //const homegameJoinConfirmation = '/%1$s/homegame/joined';
        public const string HomegameListing = "/-/homegame/listing";

        public const string PlayerAdd = "{0}/player/add";
        //const playerAddConfirmation = '/%1$s/player/created';
        public const string PlayerDelete = "/{0}/player/delete/{1}";
        public const string PlayerDetails = "/{0}/player/details/{1}";
        public const string PlayerIndex = "/{0}/player/index";
        public const string PlayerInvite = "/{0}/player/invite/{1}";
        //const playerInviteConfirmation = '/%1$s/player/invited/%2$s';

        public const string SharingSettings = "/-/sharing";

        //const twitterCallback = '%1$s/-/sharing/twittercallback';
        //const twitterSettings = '/-/sharing/twitter';
        //const twitterStartShare = '/-/sharing/twitterstart';
        //const twitterStopShare = '/-/sharing/twitterstop';

        public const string UserAdd = "/-/user/add";
        //const userAddConfirmation = '/-/user/created';
        public const string UserDetails = "/-/user/details/{0}";
        public const string UserEdit = "/-/user/edit/{0}";
        public const string UserListing = "/-/user/listing";

	}

}