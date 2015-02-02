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
using Core.UseCases.CashgameChart;
using Core.UseCases.CashgameContext;
using Core.UseCases.CashgameDetails;
using Core.UseCases.CashgameDetailsChart;
using Core.UseCases.CashgameFacts;
using Core.UseCases.CashgameHome;
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
using Core.UseCases.EventDetails;
using Core.UseCases.EventList;
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
using Core.UseCases.RequireManager;
using Core.UseCases.RequirePlayer;
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
        public Func<BaseContextResult> BaseContext { get { return BaseContextInteractor.Execute; } }
        public Func<AppContextResult> AppContext { get { return () => AppContextInteractor.Execute(BaseContext(), Auth); } }
        public Func<BunchContextRequest, BunchContextResult> BunchContext { get { return request => BunchContextInteractor.Execute(AppContext(), BunchRepository, Auth, request); } }
        public Func<CashgameContextRequest, CashgameContextResult> CashgameContext { get { return request => CashgameContextInteractor.Execute(BunchContext(request), CashgameRepository, request); } }

        // Auth and Home
        public Func<HomeResult> Home { get { return () => HomeInteractor.Execute(Auth); } }
        public Func<LoginFormRequest, LoginFormResult> LoginForm { get { return LoginFormInteractor.Execute; } }
        public Func<LoginRequest, LoginResult> Login { get { return request => LoginInteractor.Execute(UserRepository, Auth, BunchRepository, PlayerRepository, request); } }
        public Func<LogoutResult> Logout { get { return () => LogoutInteractor.Execute(Auth); } }
        public Action<RequirePlayerRequest> RequirePlayer { get { return request => new RequirePlayerInteractor(BunchRepository, UserRepository, PlayerRepository).Execute(request); } }
        public Action<RequireManagerRequest> RequireManager { get { return request => new RequireManagerInteractor(BunchRepository, UserRepository, PlayerRepository).Execute(request); } }

        // Admin
        public TestEmailInteractor TestEmail { get { return new TestEmailInteractor(MessageSender); } }
        public ClearCacheInteractor ClearCache { get { return new ClearCacheInteractor(CacheContainer); } }

        // User
        public UserListInteractor UserList { get { return new UserListInteractor(UserRepository); } }
        public UserDetailsInteractor UserDetails { get { return new UserDetailsInteractor(Auth, UserRepository); } }
        public AddUserInteractor AddUser { get { return new AddUserInteractor(UserRepository, RandomService, MessageSender); } }
        public EditUserFormInteractor EditUserForm { get { return new EditUserFormInteractor(UserRepository); } }
        public EditUserInteractor EditUser { get { return new EditUserInteractor(UserRepository); } }
        public ForgotPasswordInteractor ForgotPassword { get { return new ForgotPasswordInteractor(UserRepository, MessageSender, RandomService); } }
        public ChangePasswordInteractor ChangePassword { get { return new ChangePasswordInteractor(Auth, UserRepository, RandomService); } } 

        // Bunch
        public BunchListInteractor BunchList { get { return new BunchListInteractor(BunchRepository); } }
        public BunchDetailsInteractor BunchDetails { get { return new BunchDetailsInteractor(BunchRepository, Auth); } }
        public AddBunchFormInteractor AddBunchForm { get { return new AddBunchFormInteractor(); } }
        public AddBunchInteractor AddBunch { get { return new AddBunchInteractor(Auth, BunchRepository, PlayerRepository); } }
        public EditBunchFormInteractor EditBunchForm { get { return new EditBunchFormInteractor(BunchRepository); } }
        public EditBunchInteractor EditBunch { get { return new EditBunchInteractor(BunchRepository); } }
        public JoinBunchFormInteractor JoinBunchForm { get { return new JoinBunchFormInteractor(BunchRepository); } }
        public JoinBunchInteractor JoinBunch { get { return new JoinBunchInteractor(Auth, BunchRepository, PlayerRepository); } } 
        public JoinBunchConfirmationInteractor JoinBunchConfirmation { get { return new JoinBunchConfirmationInteractor(BunchRepository); } }

        // Events
        public EventListInteractor EventList { get { return new EventListInteractor(BunchRepository, EventRepository); } }
        public EventDetailsInteractor EventDetails { get { return new EventDetailsInteractor(EventRepository); } } 

        // Cashgame
        public Func<CashgameHomeRequest, CashgameHomeResult> CashgameHome { get { return request => CashgameHomeInteractor.Execute(request, BunchRepository, CashgameRepository); } } 
        public Func<TopListRequest, TopListResult> TopList { get { return request => TopListInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, request); } }
        public Func<CashgameDetailsRequest, CashgameDetailsResult> CashgameDetails { get { return request => CashgameDetailsInteractor.Execute(BunchRepository, CashgameRepository, Auth, PlayerRepository, request); } }
        public Func<CashgameDetailsChartRequest, CashgameDetailsChartResult> CashgameDetailsChart { get { return request => CashgameDetailsChartInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, request); } } 
        public Func<CashgameFactsRequest, CashgameFactsResult> CashgameFacts { get { return request => CashgameFactsInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, request); } }
        public Func<CashgameListRequest, CashgameListResult> CashgameList { get { return request => CashgameListInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<AddCashgameFormRequest, AddCashgameFormResult> AddCashgameForm { get { return request => AddCashgameFormInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<AddCashgameRequest, AddCashgameResult> AddCashgame { get { return request => AddCashgameInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<ActionsInput, ActionsOutput> Actions { get { return input => ActionsInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, Auth, input); } }
        public Func<ActionsChartRequest, ActionsChartResult> ActionsChart { get { return request => ActionsChartInteractor.Execute(BunchRepository, CashgameRepository, request); } } 
        public Func<EditCheckpointFormRequest, EditCheckpointFormResult> EditCheckpointForm { get { return request => EditCheckpointFormInteractor.Execute(BunchRepository, CheckpointRepository, request); } }
        public Func<EditCheckpointRequest, EditCheckpointResult> EditCheckpoint { get { return request => EditCheckpointInteractor.Execute(BunchRepository, CheckpointRepository, request); } } 
        public Func<CashgameChartRequest, CashgameChartResult> CashgameChart { get { return request => CashgameChartInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, request); } } 
        public Func<MatrixRequest, MatrixResult> Matrix { get { return request => MatrixInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, request); } }
        public Func<RunningCashgameRequest, RunningCashgameResult> RunningCashgame { get { return request => RunningCashgameInteractor.Execute(Auth, BunchRepository, CashgameRepository, PlayerRepository, request); } }
        public Func<EditCashgameFormRequest, EditCashgameFormResult> EditCashgameForm { get { return request => EditCashgameFormInteractor.Execute(BunchRepository, CashgameRepository, request); }}
        public Func<EditCashgameRequest, EditCashgameResult> EditCashgame { get { return request => EditCashgameInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<DeleteCashgameRequest, DeleteCashgameResult> DeleteCashgame { get { return request => DeleteCashgameInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<DeleteCheckpointRequest, DeleteCheckpointResult> DeleteCheckpoint { get { return request => DeleteCheckpointInteractor.Execute(BunchRepository, CashgameRepository, CheckpointRepository, request); } }
        public Action<BuyinRequest> Buyin { get { return request => BuyinInteractor.Execute(BunchRepository, PlayerRepository, CashgameRepository, CheckpointRepository, request); } }
        public Action<ReportRequest> Report { get { return request => ReportInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, CheckpointRepository, request); } }
        public Action<CashoutRequest> Cashout { get { return request => CashoutInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, CheckpointRepository, request); } }
        public Action<EndCashgameRequest> EndCashgame { get { return request => EndCashgameInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        
        // Player
        public PlayerListInteractor PlayerList { get { return new PlayerListInteractor(BunchRepository, PlayerRepository, Auth); } }
        public PlayerDetailsInteractor PlayerDetails { get { return new PlayerDetailsInteractor(Auth, BunchRepository, PlayerRepository, CashgameRepository, UserRepository); } }
        public PlayerFactsInteractor PlayerFacts { get { return new PlayerFactsInteractor(BunchRepository, CashgameRepository); } }
        public PlayerBadgesInteractor PlayerBadges { get { return new PlayerBadgesInteractor(BunchRepository, CashgameRepository); } }
        public InvitePlayerInteractor InvitePlayer { get { return new InvitePlayerInteractor(BunchRepository, PlayerRepository, MessageSender); } }
        public AddPlayerInteractor AddPlayer { get { return new AddPlayerInteractor(BunchRepository, PlayerRepository); } }
        public DeletePlayerInteractor DeletePlayer { get { return new DeletePlayerInteractor(PlayerRepository, CashgameRepository); } }
    }
}