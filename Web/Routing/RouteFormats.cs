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
        //const cashgameDetails = '/%1$s/cashgame/details/%2$s';
        //const cashgameDetailsChartJson = '/%1$s/cashgame/detailschartjson/%2$s';
        //const cashgameEdit = '/%1$s/cashgame/edit/%2$s';
        //const cashgameEnd = '/%1$s/cashgame/end';
        public const string CashgameIndex = "/{0}/cashgame/index";
        //const cashgameLeaderboard = '/%1$s/cashgame/leaderboard';
        //const cashgameLeaderboardWithYear = '/%1$s/cashgame/leaderboard/%2$s';
        //const cashgameMatrix = '/%1$s/cashgame/matrix';
        //const cashgameMatrixWithYear = '/%1$s/cashgame/matrix/%2$s';
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

        public const string HomegameAdd = "/-/game/add";
        //const homegameAddConfirmation = '/-/game/created';
        public const string HomegameDetails = "/{0}/game/details";
        //const homegameEdit = '/%1$s/game/edit';
        //const homegameJoin = '/%1$s/game/join';
        //const homegameJoinConfirmation = '/%1$s/game/joined';
        public const string HomegameListing = "/-/game/listing";

        //const playerAdd = '/%1$s/player/add';
        //const playerAddConfirmation = '/%1$s/player/created';
        //const playerDelete = '/%1$s/player/delete/%2$s';
        //const playerDetails = '/%1$s/player/details/%2$s';
        public const string PlayerIndex = "/{0}/player/index";
        //const playerInvite = '/%1$s/player/invite/%2$s';
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