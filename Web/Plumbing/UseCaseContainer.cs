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
using Core.UseCases.CashgameCurrentRankings;
using Core.UseCases.CashgameDetails;
using Core.UseCases.CashgameDetailsChart;
using Core.UseCases.CashgameFacts;
using Core.UseCases.CashgameList;
using Core.UseCases.CashgameStatus;
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
using Core.UseCases.InvitePlayer;
using Core.UseCases.JoinBunch;
using Core.UseCases.JoinBunchConfirmation;
using Core.UseCases.JoinBunchForm;
using Core.UseCases.Login;
using Core.UseCases.LoginForm;
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
        public BaseContextInteractor BaseContext { get { return new BaseContextInteractor(); } }
        public AppContextInteractor AppContext { get { return new AppContextInteractor(UserRepository); } }
        public BunchContextInteractor BunchContext { get { return new BunchContextInteractor(UserRepository, BunchRepository, PlayerRepository); } }
        public CashgameContextInteractor CashgameContext { get { return new CashgameContextInteractor(UserRepository, BunchRepository, CashgameRepository, PlayerRepository); } }

        // Auth and Home
        public LoginFormInteractor LoginForm { get { return new LoginFormInteractor(); } }
        public LoginInteractor Login { get { return new LoginInteractor(UserRepository); } }

        // Admin
        public TestEmailInteractor TestEmail { get { return new TestEmailInteractor(MessageSender); } }
        public ClearCacheInteractor ClearCache { get { return new ClearCacheInteractor(CacheContainer); } }

        // User
        public UserListInteractor UserList { get { return new UserListInteractor(UserRepository); } }
        public UserDetailsInteractor UserDetails { get { return new UserDetailsInteractor(UserRepository); } }
        public AddUserInteractor AddUser { get { return new AddUserInteractor(UserRepository, RandomService, MessageSender); } }
        public EditUserFormInteractor EditUserForm { get { return new EditUserFormInteractor(UserRepository); } }
        public EditUserInteractor EditUser { get { return new EditUserInteractor(UserRepository); } }
        public ForgotPasswordInteractor ForgotPassword { get { return new ForgotPasswordInteractor(UserRepository, MessageSender, RandomService); } }
        public ChangePasswordInteractor ChangePassword { get { return new ChangePasswordInteractor(UserRepository, RandomService); } } 

        // Bunch
        public BunchListInteractor BunchList { get { return new BunchListInteractor(BunchRepository, UserRepository); } }
        public BunchDetailsInteractor BunchDetails { get { return new BunchDetailsInteractor(BunchRepository, UserRepository, PlayerRepository); } }
        public AddBunchFormInteractor AddBunchForm { get { return new AddBunchFormInteractor(); } }
        public AddBunchInteractor AddBunch { get { return new AddBunchInteractor(UserRepository, BunchRepository, PlayerRepository); } }
        public EditBunchFormInteractor EditBunchForm { get { return new EditBunchFormInteractor(BunchRepository); } }
        public EditBunchInteractor EditBunch { get { return new EditBunchInteractor(BunchRepository); } }
        public JoinBunchFormInteractor JoinBunchForm { get { return new JoinBunchFormInteractor(BunchRepository); } }
        public JoinBunchInteractor JoinBunch { get { return new JoinBunchInteractor(BunchRepository, PlayerRepository, UserRepository); } } 
        public JoinBunchConfirmationInteractor JoinBunchConfirmation { get { return new JoinBunchConfirmationInteractor(BunchRepository); } }

        // Events
        public EventListInteractor EventList { get { return new EventListInteractor(BunchRepository, EventRepository); } }
        public EventDetailsInteractor EventDetails { get { return new EventDetailsInteractor(EventRepository); } } 

        // Cashgame
        public CashgameStatusInteractor CashgameStatus { get { return new CashgameStatusInteractor(BunchRepository, CashgameRepository); } }
        public TopListInteractor TopList { get { return new TopListInteractor(BunchRepository, CashgameRepository, PlayerRepository); } }
        public CurrentRankingsInteractor CurrentRankings { get { return new CurrentRankingsInteractor(BunchRepository, CashgameRepository, PlayerRepository); } }
        public CashgameDetailsInteractor CashgameDetails { get { return new CashgameDetailsInteractor(BunchRepository, CashgameRepository, UserRepository, PlayerRepository); } }
        public CashgameDetailsChartInteractor CashgameDetailsChart { get { return new CashgameDetailsChartInteractor(BunchRepository, CashgameRepository, PlayerRepository); } }
        public CashgameFactsInteractor CashgameFacts { get { return new CashgameFactsInteractor(BunchRepository, CashgameRepository, PlayerRepository); } }
        public CashgameListInteractor CashgameList { get { return new CashgameListInteractor(BunchRepository, CashgameRepository); } }
        public AddCashgameFormInteractor AddCashgameForm { get { return new AddCashgameFormInteractor(BunchRepository, CashgameRepository); } }
        public AddCashgameInteractor AddCashgame { get { return new AddCashgameInteractor(BunchRepository, CashgameRepository); } }
        public ActionsInteractor Actions { get { return new ActionsInteractor(BunchRepository, CashgameRepository, PlayerRepository, UserRepository); } }
        public ActionsChartInteractor ActionsChart { get { return new ActionsChartInteractor(BunchRepository, CashgameRepository); } }
        public EditCheckpointFormInteractor EditCheckpointForm { get { return new EditCheckpointFormInteractor(BunchRepository, CheckpointRepository); } }
        public EditCheckpointInteractor EditCheckpoint { get { return new EditCheckpointInteractor(BunchRepository, CheckpointRepository); } }
        public CashgameChartInteractor CashgameChart { get { return new CashgameChartInteractor(BunchRepository, CashgameRepository, PlayerRepository); } }
        public MatrixInteractor Matrix { get { return new MatrixInteractor(BunchRepository, CashgameRepository, PlayerRepository); } }
        public RunningCashgameInteractor RunningCashgame { get { return new RunningCashgameInteractor(BunchRepository, CashgameRepository, PlayerRepository, UserRepository); } }
        public EditCashgameFormInteractor EditCashgameForm { get { return new EditCashgameFormInteractor(BunchRepository, CashgameRepository); } }
        public EditCashgameInteractor EditCashgame { get { return new EditCashgameInteractor(BunchRepository, CashgameRepository); } }
        public DeleteCashgameInteractor DeleteCashgame { get { return new DeleteCashgameInteractor(CashgameRepository); } }
        public DeleteCheckpointInteractor DeleteCheckpoint { get { return new DeleteCheckpointInteractor(BunchRepository, CashgameRepository, CheckpointRepository); } }
        public BuyinInteractor Buyin { get { return new BuyinInteractor(BunchRepository, PlayerRepository, CashgameRepository, CheckpointRepository); } }
        public ReportInteractor Report { get { return new ReportInteractor(BunchRepository, CashgameRepository, PlayerRepository, CheckpointRepository); } }
        public CashoutInteractor Cashout { get { return new CashoutInteractor(BunchRepository, CashgameRepository, PlayerRepository, CheckpointRepository); } }
        public EndCashgameInteractor EndCashgame { get { return new EndCashgameInteractor(BunchRepository, CashgameRepository); } }
        
        // Player
        public PlayerListInteractor PlayerList { get { return new PlayerListInteractor(BunchRepository, UserRepository, PlayerRepository); } }
        public PlayerDetailsInteractor PlayerDetails { get { return new PlayerDetailsInteractor(BunchRepository, PlayerRepository, CashgameRepository, UserRepository); } }
        public PlayerFactsInteractor PlayerFacts { get { return new PlayerFactsInteractor(BunchRepository, CashgameRepository); } }
        public PlayerBadgesInteractor PlayerBadges { get { return new PlayerBadgesInteractor(BunchRepository, CashgameRepository); } }
        public InvitePlayerInteractor InvitePlayer { get { return new InvitePlayerInteractor(BunchRepository, PlayerRepository, MessageSender); } }
        public AddPlayerInteractor AddPlayer { get { return new AddPlayerInteractor(BunchRepository, PlayerRepository); } }
        public DeletePlayerInteractor DeletePlayer { get { return new DeletePlayerInteractor(PlayerRepository, CashgameRepository); } }
    }
}