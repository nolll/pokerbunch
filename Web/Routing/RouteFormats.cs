namespace Web.Routing{

	public class RouteFormats{

        public const string Home = "/";

        public const string AuthLogin = "/-/auth/login";
        public const string AuthLogout = "/-/auth/logout";

        public const string CashgameAdd = "/{0}/cashgame/add";
        //const cashgameAddResult = '/%1$s/cashgame/addresult/%2$s';
        //const cashgameChart = '/%1$s/cashgame/chart';
        //const cashgameChartWithYear = '/%1$s/cashgame/chart/%2$s';
        //const cashgameChartJson = '/%1$s/cashgame/chartjson';
        //const cashgameChartJsonWithYear = '/%1$s/cashgame/chartjson/%2$s';
        //const cashgameDelete = '/%1$s/cashgame/delete/%2$s';
        public const string CashgameDetails = "/{0}/cashgame/details/{1}";
        //const cashgameDetailsChartJson = '/%1$s/cashgame/detailschartjson/%2$s';
        //const cashgameEdit = '/%1$s/cashgame/edit/%2$s';
        //const cashgameEnd = '/%1$s/cashgame/end';
        public const string CashgameIndex = "/{0}/cashgame/index";
        //const cashgameLeaderboard = '/%1$s/cashgame/leaderboard';
        //const cashgameLeaderboardWithYear = '/%1$s/cashgame/leaderboard/%2$s';
        public const string CashgameMatrix = "/{0}/cashgame/matrix";
        public const string CashgameMatrixWithYear = "/{0}/cashgame/matrix/{1}";
        //const cashgameListing = '/%1$s/cashgame/listing';
        //const cashgameListingWithYear = '/%1$s/cashgame/listing/%2$s';
        //const cashgameFacts = '/%1$s/cashgame/facts';
        //const cashgameFactsWithYear = '/%1$s/cashgame/facts/%2$s';
        //const cashgameAction = '/%1$s/cashgame/action/%2$s/%3$s';
        //const cashgameActionChartJson = '/%1$s/cashgame/actionchartjson/%2$s/%3$s';
        //const cashgameBuyin = '/%1$s/cashgame/buyin/%2$s';
        //const cashgameReport = '/%1$s/cashgame/report/%2$s';
        //const cashgameCashout = '/%1$s/cashgame/cashout/%2$s';
        //const cashgamePublish = '/%1$s/cashgame/publish/%2$s';
        //const cashgameRemoveResult = '/%1$s/cashgame/removeresult/%2$s';
        //const cashgameUnpublish = '/%1$s/cashgame/unpublish/%2$s';
        public const string RunningCashgame = "/{0}/cashgame/running";
        //const cashgameCheckpointDelete = '/%1$s/cashgame/deletecheckpoint/%2$s/%3$s/%4$s';

        //const changePassword = '/-/password/change';
        //const changePasswordConfirmation = '/-/password/changed';

        public const string ForgotPassword = "/-/password/forgot";
        //const forgotPasswordConfirmation = '/-/password/sent';

        public const string HomegameAdd = "/-/homegame/add";
        //const homegameAddConfirmation = '/-/homegame/created';
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
        //const userEdit = '/-/user/edit/%1$s';
        public const string UserListing = "/-/user/listing";

	}

}