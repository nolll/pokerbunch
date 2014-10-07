using System;
using Core.UseCases.Actions;
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
using Core.UseCases.CashgameChartContainer;
using Core.UseCases.CashgameContext;
using Core.UseCases.CashgameDetails;
using Core.UseCases.CashgameFacts;
using Core.UseCases.CashgameList;
using Core.UseCases.CashgameTopList;
using Core.UseCases.DeletePlayer;
using Core.UseCases.EditBunchForm;
using Core.UseCases.EditCheckpointForm;
using Core.UseCases.EditUserForm;
using Core.UseCases.ForgotPassword;
using Core.UseCases.Home;
using Core.UseCases.InvitePlayer;
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
using Core.UseCases.RunningCashgame;
using Core.UseCases.TestEmail;
using Core.UseCases.UserDetails;
using Core.UseCases.UserList;

namespace Plumbing
{
    public class UseCaseContainer : Dependencies
    {
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

        // User
        public Func<UserListResult> UserList { get { return () => UserListInteractor.Execute(UserRepository); } }
        public Func<UserDetailsRequest, UserDetailsResult> UserDetails { get { return request => UserDetailsInteractor.Execute(Auth, UserRepository, request); } }
        public Func<AddUserRequest, AddUserResult> AddUser { get { return request => AddUserInteractor.Execute(UserRepository, RandomService, MessageSender, request); } }
        public Func<EditUserFormRequest, EditUserFormResult> EditUserForm { get { return request => EditUserFormInteractor.Execute(UserRepository, request); } }
        public Func<ForgotPasswordRequest, ForgotPasswordResult> ForgotPassword { get { return request => ForgotPasswordInteractor.Execute(UserRepository, MessageSender, RandomService, request); } }

        // Bunch
        public Func<BunchListResult> BunchList { get { return () => BunchListInteractor.Execute(BunchRepository); } }
        public Func<BunchDetailsRequest, BunchDetailsResult> BunchDetails { get { return request => BunchDetailsInteractor.Execute(BunchRepository, Auth, request); } }
        public Func<AddBunchFormResult> AddBunchForm { get { return AddBunchFormInteractor.Execute; } }
        public Func<EditBunchFormRequest, EditBunchFormResult> EditBunchForm { get { return request => EditBunchFormInteractor.Execute(BunchRepository, request); } }
        public Func<JoinBunchFormRequest, JoinBunchFormResult> JoinBunchForm { get { return request => JoinBunchFormInteractor.Execute(BunchRepository, request); } }
        public Func<JoinBunchConfirmationRequest, JoinBunchConfirmationResult> JoinBunchConfirmation { get { return request => JoinBunchConfirmationInteractor.Execute(BunchRepository, request); } }

        // Cashgame
        public Func<TopListRequest, TopListResult> TopList { get { return request => TopListInteractor.Execute(BunchRepository, CashgameService, request); } }
        public Func<CashgameDetailsRequest, CashgameDetailsResult> CashgameDetails { get { return request => CashgameDetailsInteractor.Execute(BunchRepository, CashgameRepository, Auth, PlayerRepository, request); } }
        public Func<CashgameFactsRequest, CashgameFactsResult> CashgameFacts { get { return request => CashgameFactsInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, request); } }
        public Func<CashgameListRequest, CashgameListResult> CashgameList { get { return request => CashgameListInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<AddCashgameFormRequest, AddCashgameFormResult> AddCashgameForm { get { return request => AddCashgameFormInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<AddCashgameRequest, AddCashgameResult> AddCashgame { get { return request => AddCashgameInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<ActionsRequest, ActionsResult> Actions { get { return request => ActionsInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, Auth, request); } }
        public Func<BuyinFormRequest, BuyinFormResult> BuyinForm { get { return request => BuyinFormInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<BuyinRequest, BuyinResult> Buyin { get { return request => BuyinInteractor.Execute(BunchRepository, PlayerRepository, CashgameRepository, CheckpointRepository, TimeProvider, request); } }
        public Func<EditCheckpointFormRequest, EditCheckpointFormResult> EditCheckpointForm { get { return request => EditCheckpointFormInteractor.Execute(BunchRepository, CheckpointRepository, request); } }
        public Func<CashgameChartContainerRequest, CashgameChartContainerResult> CashgameChartContainer { get { return CashgameChartContainerInteractor.Execute; } }
        public Func<MatrixRequest, MatrixResult> Matrix { get { return request => MatrixInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, request); } }
        public Func<RunningCashgameRequest, RunningCashgameResult> RunningCashgame { get { return request => RunningCashgameInteractor.Execute(Auth, BunchRepository, CashgameRepository, PlayerRepository, TimeProvider, request); } } 

        // Player
        public Func<PlayerListRequest, PlayerListResult> PlayerList { get { return request => PlayerListInteractor.Execute(BunchRepository, PlayerRepository, Auth, request); } }
        public Func<PlayerDetailsRequest, PlayerDetailsResult> PlayerDetails { get { return request => PlayerDetailsInteractor.Execute(Auth, BunchRepository, PlayerRepository, CashgameRepository, UserRepository, request); } }
        public Func<PlayerFactsRequest, PlayerFactsResult> PlayerFacts { get { return request => PlayerFactsInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<PlayerBadgesRequest, PlayerBadgesResult> PlayerBadges { get { return request => PlayerBadgesInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<InvitePlayerRequest, InvitePlayerResult> InvitePlayer { get { return request => InvitePlayerInteractor.Execute(BunchRepository, PlayerRepository, MessageSender, request); } }
        public Func<AddPlayerRequest, AddPlayerResult> AddPlayer { get { return request => AddPlayerInteractor.Execute(BunchRepository, PlayerRepository, request); } }
        public Func<DeletePlayerRequest, DeletePlayerResult> DeletePlayer { get { return request => DeletePlayerInteractor.Execute(BunchRepository, PlayerRepository, CashgameRepository, request); } }
    }
}