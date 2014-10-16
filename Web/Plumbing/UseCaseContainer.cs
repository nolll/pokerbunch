using System;
using Core.UseCases.Actions;
using Core.UseCases.ActionsChart;
using Core.UseCases.AddBunch;
using Core.UseCases.AddBunchForm;
using Core.UseCases.AddCashgame;
using Core.UseCases.AddCashgameForm;
using Core.UseCases.AddPlayer;
using Core.UseCases.AddUser;
using Core.UseCases.AppContext;
using Core.UseCases.BaseContext;
using Core.UseCases.BunchContext;
using Core.UseCases.BunchDetails;
using Core.UseCases.BunchList;
using Core.UseCases.Buyin;
using Core.UseCases.BuyinForm;
using Core.UseCases.CashgameChart;
using Core.UseCases.CashgameChartContainer;
using Core.UseCases.CashgameContext;
using Core.UseCases.CashgameDetails;
using Core.UseCases.CashgameDetailsChart;
using Core.UseCases.CashgameFacts;
using Core.UseCases.CashgameList;
using Core.UseCases.CashgameTopList;
using Core.UseCases.Cashout;
using Core.UseCases.ChangePassword;
using Core.UseCases.ClearCache;
using Core.UseCases.DeleteCashgame;
using Core.UseCases.DeleteCheckpoint;
using Core.UseCases.DeletePlayer;
using Core.UseCases.EditBunch;
using Core.UseCases.EditBunchForm;
using Core.UseCases.EditCashgame;
using Core.UseCases.EditCashgameForm;
using Core.UseCases.EditCheckpoint;
using Core.UseCases.EditCheckpointForm;
using Core.UseCases.EditUser;
using Core.UseCases.EditUserForm;
using Core.UseCases.EndCashgame;
using Core.UseCases.ForgotPassword;
using Core.UseCases.Home;
using Core.UseCases.InvitePlayer;
using Core.UseCases.JoinBunch;
using Core.UseCases.JoinBunchConfirmation;
using Core.UseCases.JoinBunchForm;
using Core.UseCases.Login;
using Core.UseCases.LoginForm;
using Core.UseCases.Logout;
using Core.UseCases.Matrix;
using Core.UseCases.PlayerBadges;
using Core.UseCases.PlayerDetails;
using Core.UseCases.PlayerFacts;
using Core.UseCases.PlayerList;
using Core.UseCases.Report;
using Core.UseCases.RunningCashgame;
using Core.UseCases.TestEmail;
using Core.UseCases.UserDetails;
using Core.UseCases.UserList;
using Plumbing;

namespace Web.Plumbing
{
    public class UseCaseContainer : Dependencies
    {
        // Contexts
        public Func<BaseContextResult> BaseContext { get { return () => BaseContextInteractor.Execute(WebContext); } }
        public Func<AppContextResult> AppContext { get { return () => AppContextInteractor.Execute(BaseContext, Auth); } }
        public Func<BunchContextRequest, BunchContextResult> BunchContext { get { return request => BunchContextInteractor.Execute(AppContext, BunchRepository, Auth, request); } }
        public Func<CashgameContextRequest, CashgameContextResult> CashgameContext { get { return request => CashgameContextInteractor.Execute(BunchContext, CashgameRepository, request); } }

        // Auth and Home
        public Func<HomeResult> Home { get { return () => HomeInteractor.Execute(Auth); } }
        public Func<LoginFormRequest, LoginFormResult> LoginForm { get { return LoginFormInteractor.Execute; } }
        public Func<LoginRequest, LoginResult> Login { get { return request => LoginInteractor.Execute(UserRepository, Auth, BunchRepository, PlayerRepository, request); } }
        public Func<LogoutResult> Logout { get { return () => LogoutInteractor.Execute(Auth); } }

        // Admin
        public Func<TestEmailResult> TestEmail { get { return () => TestEmailInteractor.Execute(MessageSender); } }
        public Func<ClearCacheOutput> ClearCache { get { return () => ClearCacheInteractor.Execute(CacheContainer); } } 

        // User
        public Func<UserListResult> UserList { get { return () => UserListInteractor.Execute(UserRepository); } }
        public Func<UserDetailsRequest, UserDetailsResult> UserDetails { get { return request => UserDetailsInteractor.Execute(Auth, UserRepository, request); } }
        public Func<AddUserRequest, AddUserResult> AddUser { get { return request => AddUserInteractor.Execute(UserRepository, RandomService, MessageSender, request); } }
        public Func<EditUserFormRequest, EditUserFormResult> EditUserForm { get { return request => EditUserFormInteractor.Execute(UserRepository, request); } }
        public Func<EditUserRequest, EditUserResult> EditUser { get { return request => EditUserInteractor.Execute(UserRepository, request); } } 
        public Func<ForgotPasswordRequest, ForgotPasswordResult> ForgotPassword { get { return request => ForgotPasswordInteractor.Execute(UserRepository, MessageSender, RandomService, request); } }
        public Func<ChangePasswordRequest, ChangePasswordResult> ChangePassword { get { return request => ChangePasswordInteractor.Execute(Auth, UserRepository, RandomService, request); } } 

        // Bunch
        public Func<BunchListResult> BunchList { get { return () => BunchListInteractor.Execute(BunchRepository); } }
        public Func<BunchDetailsRequest, BunchDetailsResult> BunchDetails { get { return request => BunchDetailsInteractor.Execute(BunchRepository, Auth, request); } }
        public Func<AddBunchFormResult> AddBunchForm { get { return AddBunchFormInteractor.Execute; } }
        public Func<AddBunchRequest, AddBunchResult> AddBunch { get { return request => AddBunchInteractor.Execute(Auth, BunchRepository, PlayerRepository, request); } }
        public Func<EditBunchFormRequest, EditBunchFormResult> EditBunchForm { get { return request => EditBunchFormInteractor.Execute(BunchRepository, request); } }
        public Func<EditBunchRequest, EditBunchResult> EditBunch { get { return request => EditBunchInteractor.Execute(BunchRepository, request); } }
        public Func<JoinBunchFormRequest, JoinBunchFormResult> JoinBunchForm { get { return request => JoinBunchFormInteractor.Execute(BunchRepository, request); } }
        public Func<JoinBunchRequest, JoinBunchResult> JoinBunch { get { return request => JoinBunchInteractor.Execute(Auth, BunchRepository, PlayerRepository, request); } } 
        public Func<JoinBunchConfirmationRequest, JoinBunchConfirmationResult> JoinBunchConfirmation { get { return request => JoinBunchConfirmationInteractor.Execute(BunchRepository, request); } }

        // Cashgame
        public Func<TopListRequest, TopListResult> TopList { get { return request => TopListInteractor.Execute(BunchRepository, CashgameService, request); } }
        public Func<CashgameDetailsRequest, CashgameDetailsResult> CashgameDetails { get { return request => CashgameDetailsInteractor.Execute(BunchRepository, CashgameRepository, Auth, PlayerRepository, request); } }
        public Func<CashgameDetailsChartRequest, CashgameDetailsChartResult> CashgameDetailsChart { get { return request => CashgameDetailsChartInteractor.Execute(TimeProvider, CashgameService, BunchRepository, CashgameRepository, request); } } 
        public Func<CashgameFactsRequest, CashgameFactsResult> CashgameFacts { get { return request => CashgameFactsInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, request); } }
        public Func<CashgameListRequest, CashgameListResult> CashgameList { get { return request => CashgameListInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<AddCashgameFormRequest, AddCashgameFormResult> AddCashgameForm { get { return request => AddCashgameFormInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<AddCashgameRequest, AddCashgameResult> AddCashgame { get { return request => AddCashgameInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<ActionsInput, ActionsOutput> Actions { get { return input => ActionsInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, Auth, input); } }
        public Func<ActionsChartRequest, ActionsChartResult> ActionsChart { get { return request => ActionsChartInteractor.Execute(TimeProvider, BunchRepository, CashgameRepository, request); } } 
        public Func<BuyinFormRequest, BuyinFormResult> BuyinForm { get { return request => BuyinFormInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<BuyinRequest, BuyinResult> Buyin { get { return request => BuyinInteractor.Execute(BunchRepository, PlayerRepository, CashgameRepository, CheckpointRepository, TimeProvider, request); } }
        public Func<EndCashgameRequest, EndCashgameResult> EndCashgame { get { return request => EndCashgameInteractor.Execute(BunchRepository, CashgameRepository, request); } } 
        public Func<EditCheckpointFormRequest, EditCheckpointFormResult> EditCheckpointForm { get { return request => EditCheckpointFormInteractor.Execute(BunchRepository, CheckpointRepository, request); } }
        public Func<EditCheckpointRequest, EditCheckpointResult> EditCheckpoint { get { return request => EditCheckpointInteractor.Execute(BunchRepository, CashgameRepository, CheckpointRepository, request); } } 
        public Func<CashgameChartContainerRequest, CashgameChartContainerResult> CashgameChartContainer { get { return CashgameChartContainerInteractor.Execute; } }
        public Func<CashgameChartRequest, CashgameChartResult> CashgameChart { get { return request => CashgameChartInteractor.Execute(BunchRepository, CashgameService, request); } } 
        public Func<MatrixRequest, MatrixResult> Matrix { get { return request => MatrixInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, request); } }
        public Func<RunningCashgameRequest, RunningCashgameResult> RunningCashgame { get { return request => RunningCashgameInteractor.Execute(Auth, BunchRepository, CashgameRepository, PlayerRepository, TimeProvider, request); } }
        public Func<EditCashgameFormRequest, EditCashgameFormResult> EditCashgameForm { get { return request => EditCashgameFormInteractor.Execute(BunchRepository, CashgameRepository, request); }}
        public Func<EditCashgameRequest, EditCashgameResult> EditCashgame { get { return request => EditCashgameInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<ReportRequest, ReportResult> Report { get { return request => ReportInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, CheckpointRepository, TimeProvider, request); } }
        public Func<CashoutRequest, CashoutResult> Cashout { get { return request => CashoutInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, CheckpointRepository, TimeProvider, request); } }
        public Func<DeleteCashgameRequest, DeleteCashgameResult> DeleteCashgame { get { return request => DeleteCashgameInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<DeleteCheckpointRequest, DeleteCheckpointResult> DeleteCheckpoint { get { return request => DeleteCheckpointInteractor.Execute(BunchRepository, CashgameRepository, CheckpointRepository, request); } } 

        // Player
        public Func<PlayerListRequest, PlayerListResult> PlayerList { get { return request => PlayerListInteractor.Execute(BunchRepository, PlayerRepository, Auth, request); } }
        public Func<PlayerDetailsRequest, PlayerDetailsResult> PlayerDetails { get { return request => PlayerDetailsInteractor.Execute(Auth, BunchRepository, PlayerRepository, CashgameRepository, UserRepository, request); } }
        public Func<PlayerFactsRequest, PlayerFactsResult> PlayerFacts { get { return request => PlayerFactsInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<PlayerBadgesRequest, PlayerBadgesResult> PlayerBadges { get { return request => PlayerBadgesInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<InvitePlayerRequest, InvitePlayerResult> InvitePlayer { get { return request => InvitePlayerInteractor.Execute(BunchRepository, PlayerRepository, MessageSender, request); } }
        public Func<AddPlayerRequest, AddPlayerResult> AddPlayer { get { return request => AddPlayerInteractor.Execute(BunchRepository, PlayerRepository, request); } }
        public Func<DeletePlayerRequest, DeletePlayerResult> DeletePlayer { get { return request => DeletePlayerInteractor.Execute(BunchRepository, PlayerRepository, CashgameRepository, request); } }

        private static UseCaseContainer _instance;
        private static readonly object Padlock = new object();

        public static UseCaseContainer Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ?? (_instance = new UseCaseContainer());
                }
            }
        }

        private UseCaseContainer()
        {
        }
    }
}