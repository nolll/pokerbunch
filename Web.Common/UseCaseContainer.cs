using Core.UseCases;
using Core.UseCases.CashgameCurrentRankings;
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
using Core.UseCases.EventList;
using Core.UseCases.ForgotPassword;
using Core.UseCases.InvitePlayer;
using Core.UseCases.JoinBunch;
using Core.UseCases.JoinBunchConfirmation;
using Core.UseCases.JoinBunchForm;
using Core.UseCases.Login;
using Core.UseCases.Matrix;
using Core.UseCases.PlayerBadges;
using Core.UseCases.PlayerFacts;
using Core.UseCases.PlayerList;
using Core.UseCases.Report;
using Core.UseCases.RunningCashgame;
using Core.UseCases.UserList;
using Plumbing;

namespace Web.Common
{
    public class UseCaseContainer
    {
        private readonly Dependencies _deps = new Dependencies();

        // Contexts
        public BaseContext BaseContext { get { return new BaseContext(); } }
        public AppContext AppContext { get { return new AppContext(_deps.UserRepository); } }
        public BunchContext BunchContext { get { return new BunchContext(_deps.UserRepository, _deps.BunchRepository, _deps.PlayerRepository); } }
        public CashgameContext CashgameContext { get { return new CashgameContext(_deps.UserRepository, _deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }

        // Auth and Home
        public LoginForm LoginForm { get { return new LoginForm(); } }
        public LoginInteractor Login { get { return new LoginInteractor(_deps.UserRepository); } }

        // Admin
        public TestEmail TestEmail { get { return new TestEmail(_deps.MessageSender); } }
        public ClearCacheInteractor ClearCache { get { return new ClearCacheInteractor(_deps.CacheContainer); } }

        // User
        public UserListInteractor UserList { get { return new UserListInteractor(_deps.UserRepository); } }
        public UserDetails UserDetails { get { return new UserDetails(_deps.UserRepository); } }
        public AddUser AddUser { get { return new AddUser(_deps.UserRepository, _deps.RandomService, _deps.MessageSender); } }
        public EditUserFormInteractor EditUserForm { get { return new EditUserFormInteractor(_deps.UserRepository); } }
        public EditUserInteractor EditUser { get { return new EditUserInteractor(_deps.UserRepository); } }
        public ForgotPasswordInteractor ForgotPassword { get { return new ForgotPasswordInteractor(_deps.UserRepository, _deps.MessageSender, _deps.RandomService); } }
        public ChangePasswordInteractor ChangePassword { get { return new ChangePasswordInteractor(_deps.UserRepository, _deps.RandomService); } } 

        // Bunch
        public BunchList BunchList { get { return new BunchList(_deps.BunchRepository, _deps.UserRepository); } }
        public BunchDetails BunchDetails { get { return new BunchDetails(_deps.BunchRepository, _deps.UserRepository, _deps.PlayerRepository); } }
        public AddBunchForm AddBunchForm { get { return new AddBunchForm(); } }
        public AddBunch AddBunch { get { return new AddBunch(_deps.UserRepository, _deps.BunchRepository, _deps.PlayerRepository); } }
        public EditBunchFormInteractor EditBunchForm { get { return new EditBunchFormInteractor(_deps.BunchRepository); } }
        public EditBunchInteractor EditBunch { get { return new EditBunchInteractor(_deps.BunchRepository); } }
        public JoinBunchFormInteractor JoinBunchForm { get { return new JoinBunchFormInteractor(_deps.BunchRepository); } }
        public JoinBunchInteractor JoinBunch { get { return new JoinBunchInteractor(_deps.BunchRepository, _deps.PlayerRepository, _deps.UserRepository); } }
        public JoinBunchConfirmationInteractor JoinBunchConfirmation { get { return new JoinBunchConfirmationInteractor(_deps.BunchRepository); } }

        // Events
        public EventListInteractor EventList { get { return new EventListInteractor(_deps.BunchRepository, _deps.EventRepository); } }
        public EventDetails EventDetails { get { return new EventDetails(_deps.EventRepository); } } 

        // Cashgame
        public CashgameStatus CashgameStatus { get { return new CashgameStatus(_deps.BunchRepository, _deps.CashgameRepository); } }
        public TopListInteractor TopList { get { return new TopListInteractor(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
        public CurrentRankings CurrentRankings { get { return new CurrentRankings(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
        public CashgameDetails CashgameDetails { get { return new CashgameDetails(_deps.BunchRepository, _deps.CashgameRepository, _deps.UserRepository, _deps.PlayerRepository); } }
        public CashgameDetailsChart CashgameDetailsChart { get { return new CashgameDetailsChart(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
        public CashgameFacts CashgameFacts { get { return new CashgameFacts(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
        public CashgameList CashgameList { get { return new CashgameList(_deps.BunchRepository, _deps.CashgameRepository); } }
        public AddCashgameForm AddCashgameForm { get { return new AddCashgameForm(_deps.BunchRepository, _deps.CashgameRepository); } }
        public AddCashgame AddCashgame { get { return new AddCashgame(_deps.BunchRepository, _deps.CashgameRepository); } }
        public Actions Actions { get { return new Actions(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository); } }
        public ActionsChart ActionsChart { get { return new ActionsChart(_deps.BunchRepository, _deps.CashgameRepository); } }
        public EditCheckpointFormInteractor EditCheckpointForm { get { return new EditCheckpointFormInteractor(_deps.BunchRepository, _deps.CheckpointRepository); } }
        public EditCheckpointInteractor EditCheckpoint { get { return new EditCheckpointInteractor(_deps.BunchRepository, _deps.CheckpointRepository); } }
        public CashgameChart CashgameChart { get { return new CashgameChart(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
        public MatrixInteractor Matrix { get { return new MatrixInteractor(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
        public RunningCashgameInteractor RunningCashgame { get { return new RunningCashgameInteractor(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository); } }
        public EditCashgameFormInteractor EditCashgameForm { get { return new EditCashgameFormInteractor(_deps.BunchRepository, _deps.CashgameRepository); } }
        public EditCashgameInteractor EditCashgame { get { return new EditCashgameInteractor(_deps.BunchRepository, _deps.CashgameRepository); } }
        public DeleteCashgameInteractor DeleteCashgame { get { return new DeleteCashgameInteractor(_deps.CashgameRepository); } }
        public DeleteCheckpointInteractor DeleteCheckpoint { get { return new DeleteCheckpointInteractor(_deps.BunchRepository, _deps.CashgameRepository, _deps.CheckpointRepository); } }
        public Buyin Buyin { get { return new Buyin(_deps.BunchRepository, _deps.PlayerRepository, _deps.CashgameRepository, _deps.CheckpointRepository); } }
        public ReportInteractor Report { get { return new ReportInteractor(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.CheckpointRepository); } }
        public CashoutInteractor Cashout { get { return new CashoutInteractor(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.CheckpointRepository); } }
        public EndCashgameInteractor EndCashgame { get { return new EndCashgameInteractor(_deps.BunchRepository, _deps.CashgameRepository); } }
        
        // Player
        public PlayerListInteractor PlayerList { get { return new PlayerListInteractor(_deps.BunchRepository, _deps.UserRepository, _deps.PlayerRepository); } }
        public PlayerDetails PlayerDetails { get { return new PlayerDetails(_deps.BunchRepository, _deps.PlayerRepository, _deps.CashgameRepository, _deps.UserRepository); } }
        public PlayerFactsInteractor PlayerFacts { get { return new PlayerFactsInteractor(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
        public PlayerBadgesInteractor PlayerBadges { get { return new PlayerBadgesInteractor(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
        public InvitePlayerInteractor InvitePlayer { get { return new InvitePlayerInteractor(_deps.BunchRepository, _deps.PlayerRepository, _deps.MessageSender); } }
        public AddPlayer AddPlayer { get { return new AddPlayer(_deps.BunchRepository, _deps.PlayerRepository); } }
        public DeletePlayerInteractor DeletePlayer { get { return new DeletePlayerInteractor(_deps.PlayerRepository, _deps.CashgameRepository); } }
    }
}