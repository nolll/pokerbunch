namespace Core.Urls
{
    public static class Routes
    {
        public const string Home = "";

        public const string AuthLogin = "-/auth/login";
        public const string AuthLogout = "-/auth/logout";

        public const string CashgameAdd = "{slug}/cashgame/add";
        public const string CashgameChart = "{slug}/cashgame/chart";
        public const string CashgameChartWithYear = CashgameChart + "/{year?}";
        public const string CashgameDelete = "{slug}/cashgame/delete/{dateStr}";
        public const string CashgameDetails = "{slug}/cashgame/details/{dateStr}";
        public const string CashgameEdit = "{slug}/cashgame/edit/{dateStr}";
        public const string CashgameEnd = "{slug}/cashgame/end";
        public const string CashgameIndex = "{slug}/cashgame";
        public const string CashgameToplist = "{slug}/cashgame/toplist";
        public const string CashgameToplistWithYear = CashgameToplist + "/{year?}";
        public const string CashgameMatrix = "{slug}/cashgame/matrix";
        public const string CashgameMatrixWithYear = CashgameMatrix + "/{year?}";
        public const string CashgameList = "{slug}/cashgame/list";
        public const string CashgameListWithYear = CashgameList + "/{year?}";
        public const string CashgameFacts = "{slug}/cashgame/facts";
        public const string CashgameFactsWithYear = CashgameFacts + "/{year?}";
        public const string CashgameAction = "{slug}/cashgame/action/{dateStr}/{playerId}";
        public const string CashgameBuyin = "{slug}/cashgame/buyin";
        public const string CashgameReport = "{slug}/cashgame/report";
        public const string CashgameCashout = "{slug}/cashgame/cashout";
        public const string RunningCashgame = "{slug}/cashgame/running";
        public const string RunningCashgameGameJson = "{slug}/cashgame/runninggamejson";
        public const string RunningCashgamePlayersJson = "{slug}/cashgame/runningplayersjson";
        public const string CashgameCheckpointDelete = "{slug}/cashgame/deletecheckpoint/{dateStr}/{playerId}/{checkpointId}";
        public const string CashgameCheckpointEdit = "{slug}/cashgame/editcheckpoint/{dateStr}/{playerId}/{checkpointId}";

        public const string BunchAdd = "-/homegame/add";
        public const string BunchAddConfirmation = "-/homegame/created";
        public const string BunchDetails = "{slug}/homegame/details";
        public const string BunchEdit = "{slug}/homegame/edit";
        public const string BunchJoin = "{slug}/homegame/join";
        public const string BunchJoinConfirmation = "{slug}/homegame/joined";
        public const string BunchList = "-/homegame/list";

        public const string EventList = "{slug}/event/list";
        public const string EventDetails = "{slug}/event/details/{eventId}";

        public const string PlayerAdd = "{slug}/player/add";
        public const string PlayerAddConfirmation = "{slug}/player/created";
        public const string PlayerDelete = "{slug}/player/delete/{playerId}";
        public const string PlayerDetails = "-/player/details/{playerId}";
        public const string PlayerIndex = "{slug}/player/index";
        public const string PlayerInvite = "{slug}/player/invite/{playerId}";
        public const string PlayerInviteConfirmation = "{slug}/player/invited/{playerId}";

        public const string UserAdd = "-/user/add";
        public const string UserAddConfirmation = "-/user/created";
        public const string UserDetails = "-/user/details/{userName}";
        public const string UserEdit = "-/user/edit/{userName}";
        public const string UserList = "-/user/list";
        public const string ChangePassword = "-/user/changepassword";
        public const string ChangePasswordConfirmation = "-/user/changedpassword";
        public const string ForgotPassword = "-/user/forgotpassword";
        public const string ForgotPasswordConfirmation = "-/user/passwordsent";

        public const string AdminSendEmail = "-/admin/sendemail";
        public const string AdminClearCache = "-/admin/clearcache";

        public const string ErrorNotFound = "-/error/notfound";
        public const string ErrorUnauthorized = "-/error/unauthorized";
        public const string ErrorForbidden = "-/error/forbidden";
        public const string ErrorOther = "-/error/servererror";
    }
}