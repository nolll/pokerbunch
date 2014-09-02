namespace Application.Urls
{
    public static class RouteFormats
    {
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
        public const string CashgameToplist = "/{0}/cashgame/toplist";
        public const string CashgameToplistWithYear = "/{0}/cashgame/toplist/{1}";
        public const string CashgameMatrix = "/{0}/cashgame/matrix";
        public const string CashgameMatrixWithYear = "/{0}/cashgame/matrix/{1}";
        public const string CashgameList = "/{0}/cashgame/list";
        public const string CashgameListWithYear = "/{0}/cashgame/list/{1}";
        public const string CashgameFacts = "/{0}/cashgame/facts";
        public const string CashgameFactsWithYear = "/{0}/cashgame/facts/{1}";
        public const string CashgameAction = "/{0}/cashgame/action/{1}/{2}";
        public const string CashgameActionChartJson = "/{0}/cashgame/actionchartjson/{1}/{2}";
        public const string CashgameBuyin = "/{0}/cashgame/buyin/{1}";
        public const string CashgameReport = "/{0}/cashgame/report/{1}";
        public const string CashgameCashout = "/{0}/cashgame/cashout/{1}";
        //const cashgameRemoveResult = '/%1$s/cashgame/removeresult/%2$s';
        public const string RunningCashgame = "/{0}/cashgame/running";
        public const string CashgameCheckpointDelete = "/{0}/cashgame/deletecheckpoint/{1}/{2}/{3}";
        public const string CashgameCheckpointEdit = "/{0}/cashgame/editcheckpoint/{1}/{2}/{3}";

        public const string BunchAdd = "/-/homegame/add";
        public const string BunchAddConfirmation = "/-/homegame/created";
        public const string BunchDetails = "/{0}/homegame/details";
        public const string BunchEdit = "/{0}/homegame/edit";
        public const string BunchJoin = "/{0}/homegame/join";
        public const string BunchJoinConfirmation = "/{0}/homegame/joined";
        public const string BunchList = "/-/homegame/list";

        public const string PlayerAdd = "/{0}/player/add";
        public const string PlayerAddConfirmation = "/{0}/player/created";
        public const string PlayerDelete = "/{0}/player/delete/{1}";
        public const string PlayerDetails = "/{0}/player/details/{1}";
        public const string PlayerIndex = "/{0}/player/index";
        public const string PlayerInvite = "/{0}/player/invite/{1}";
        public const string PlayerInviteConfirmation = "/{0}/player/invited/{1}";

        public const string SharingSettings = "/-/sharing";

        public const string TwitterCallback = "/-/sharing/twittercallback";
        public const string TwitterSettings = "/-/sharing/twitter";
        public const string TwitterStartShare = "/-/sharing/twitterstart";
        public const string TwitterStopShare = "/-/sharing/twitterstop";

        public const string UserAdd = "/-/user/add";
        public const string UserAddConfirmation = "/-/user/created";
        public const string UserDetails = "/-/user/details/{0}";
        public const string UserEdit = "/-/user/edit/{0}";
        public const string UserList = "/-/user/list";
        public const string ChangePassword = "/-/user/changepassword";
        public const string ChangePasswordConfirmation = "/-/user/changedpassword";
        public const string ForgotPassword = "/-/user/forgotpassword";
        public const string ForgotPasswordConfirmation = "/-/user/passwordsent";
    }
}