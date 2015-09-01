namespace Web.Urls
{
    public static class Routes
    {
        public const string Home = "";

        public const string AuthLogin = "auth/login";
        public const string AuthLogout = "auth/logout";

        public const string CashgameAdd = "cashgame/add/{slug}";
        public const string CashgameChart = "cashgame/chart/{slug}";
        public const string CashgameChartWithYear = CashgameChart + "/{year?}";
        public const string CashgameDelete = "cashgame/delete/{id}";
        public const string CashgameDetails = "cashgame/details/{id}";
        public const string CashgameEdit = "cashgame/edit/{id}";
        public const string CashgameEnd = "cashgame/end/{slug}";
        public const string CashgameIndex = "cashgame/index/{slug}";
        public const string CashgameToplist = "cashgame/toplist/{slug}";
        public const string CashgameToplistWithYear = CashgameToplist + "/{year?}";
        public const string CashgameMatrix = "cashgame/matrix/{slug}";
        public const string CashgameMatrixWithYear = CashgameMatrix + "/{year?}";
        public const string CashgameList = "cashgame/list/{slug}";
        public const string CashgameListWithYear = CashgameList + "/{year?}";
        public const string CashgameFacts = "cashgame/facts/{slug}";
        public const string CashgameFactsWithYear = CashgameFacts + "/{year?}";
        public const string CashgameAction = "cashgame/action/{cashgameId}/{playerId}";
        public const string CashgameBuyin = "cashgame/buyin/{slug}";
        public const string CashgameReport = "cashgame/report/{slug}";
        public const string CashgameCashout = "cashgame/cashout/{slug}";
        public const string RunningCashgame = "cashgame/running/{slug}";
        public const string RunningCashgameGameJson = "cashgame/runninggamejson/{slug}";
        public const string RunningCashgamePlayersJson = "cashgame/runningplayersjson/{slug}";
        public const string CashgameCheckpointDelete = "cashgame/deletecheckpoint/{id}";
        public const string CashgameCheckpointEdit = "cashgame/editcheckpoint/{id}";

        public const string BunchAdd = "bunch/add";
        public const string BunchAddConfirmation = "bunch/created";
        public const string BunchDetails = "bunch/details/{slug}";
        public const string BunchEdit = "bunch/edit/{slug}";
        public const string BunchJoin = "bunch/join/{slug}";
        public const string BunchJoinConfirmation = "bunch/joined/{slug}";
        public const string BunchList = "bunch/list";

        public const string EventList = "event/list/{slug}";
        public const string EventDetails = "event/details/{id}";

        public const string PlayerAdd = "player/add/{slug}";
        public const string PlayerAddConfirmation = "player/created/{slug}";
        public const string PlayerDelete = "player/delete/{id}";
        public const string PlayerDetails = "player/details/{id}";
        public const string PlayerIndex = "player/index/{slug}";
        public const string PlayerInvite = "player/invite/{id}";
        public const string PlayerInviteConfirmation = "player/invited/{id}";

        public const string UserAdd = "user/add";
        public const string UserAddConfirmation = "user/created";
        public const string UserDetails = "user/details/{userName}";
        public const string UserEdit = "user/edit/{userName}";
        public const string UserList = "user/list";
        public const string ChangePassword = "user/changepassword";
        public const string ChangePasswordConfirmation = "user/changedpassword";
        public const string ForgotPassword = "user/forgotpassword";
        public const string ForgotPasswordConfirmation = "user/passwordsent";

        public const string AllApps = "app/list";
        public const string UserApps = "user/apps";
        public const string AppDetails = "app/details/{id}";
        public const string AppEdit = "app/edit/{id}";

        public const string AdminSendEmail = "admin/sendemail";
        public const string AdminClearCache = "admin/clearcache";

        public const string ErrorNotFound = "error/notfound";
        public const string ErrorUnauthorized = "error/unauthorized";
        public const string ErrorForbidden = "error/forbidden";
        public const string ErrorOther = "error/servererror";
    }
}