using Core.UseCases;
using Plumbing;

namespace Web.Common
{
    public class UseCaseContainer
    {
        private readonly Dependencies _deps;

        public UseCaseContainer(Dependencies deps)
        {
            _deps = deps;
        }

        // Contexts
        public BaseContext BaseContext { get { return new BaseContext(); } }
        public AppContext AppContext { get { return new AppContext(_deps.UserService); } }
        public BunchContext BunchContext { get { return new BunchContext(_deps.UserService, _deps.BunchService); } }
        public CashgameContext CashgameContext { get { return new CashgameContext(_deps.UserService, _deps.BunchService, _deps.CashgameService); } }

        // Auth and Home
        public LoginForm LoginForm { get { return new LoginForm(); } }
        public Login Login { get { return new Login(_deps.UserService); } }

        // Admin
        public TestEmail TestEmail { get { return new TestEmail(_deps.MessageSender, _deps.UserService); } }

        // User
        public UserList UserList { get { return new UserList(_deps.UserService); } }
        public UserDetails UserDetails { get { return new UserDetails(_deps.UserService); } }
        public AddUser AddUser { get { return new AddUser(_deps.UserService, _deps.RandomService, _deps.MessageSender); } }
        public EditUserForm EditUserForm { get { return new EditUserForm(_deps.UserService); } }
        public EditUser EditUser { get { return new EditUser(_deps.UserService); } }
        public ForgotPassword ForgotPassword { get { return new ForgotPassword(_deps.UserService, _deps.MessageSender, _deps.RandomService); } }
        public ChangePassword ChangePassword { get { return new ChangePassword(_deps.UserService, _deps.RandomService); } } 

        // Bunch
        public BunchList BunchList { get { return new BunchList(_deps.BunchService, _deps.UserService); } }
        public BunchDetails BunchDetails { get { return new BunchDetails(_deps.BunchService, _deps.UserService, _deps.PlayerService); } }
        public AddBunchForm AddBunchForm { get { return new AddBunchForm(); } }
        public AddBunch AddBunch { get { return new AddBunch(_deps.UserService, _deps.BunchService, _deps.PlayerService); } }
        public EditBunchForm EditBunchForm { get { return new EditBunchForm(_deps.BunchService, _deps.UserService, _deps.PlayerService); } }
        public EditBunch EditBunch { get { return new EditBunch(_deps.BunchService, _deps.UserService, _deps.PlayerService); } }
        public JoinBunchForm JoinBunchForm { get { return new JoinBunchForm(_deps.BunchService); } }
        public JoinBunch JoinBunch { get { return new JoinBunch(_deps.BunchService, _deps.PlayerService, _deps.UserService); } }
        public JoinBunchConfirmation JoinBunchConfirmation { get { return new JoinBunchConfirmation(_deps.BunchService, _deps.UserService, _deps.PlayerService); } }

        // Events
        public EventList EventList { get { return new EventList(_deps.BunchService, _deps.EventService, _deps.UserService, _deps.PlayerService, _deps.LocationService); } }
        public EventDetails EventDetails { get { return new EventDetails(_deps.EventService, _deps.UserService, _deps.PlayerService, _deps.BunchService); } }
        public AddEvent AddEvent { get { return new AddEvent(_deps.BunchService, _deps.PlayerService, _deps.UserService, _deps.EventService); } }

        // Cashgame
        public CashgameStatus CashgameStatus { get { return new CashgameStatus(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService); } }
        public TopList TopList { get { return new TopList(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService); } }
        public CurrentRankings CurrentRankings { get { return new CurrentRankings(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService); } }
        public CashgameDetails CashgameDetails { get { return new CashgameDetails(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationService); } }
        public CashgameDetailsChart CashgameDetailsChart { get { return new CashgameDetailsChart(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService); } }
        public CashgameFacts CashgameFacts { get { return new CashgameFacts(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService); } }
        public CashgameList CashgameList { get { return new CashgameList(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationService); } }
        public AddCashgameForm AddCashgameForm { get { return new AddCashgameForm(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationService); } }
        public AddCashgame AddCashgame { get { return new AddCashgame(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationService); } }
        public Actions Actions { get { return new Actions(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService); } }
        public ActionsChart ActionsChart { get { return new ActionsChart(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService); } }
        public EditCheckpointForm EditCheckpointForm { get { return new EditCheckpointForm(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService); } }
        public EditCheckpoint EditCheckpoint { get { return new EditCheckpoint(_deps.BunchService, _deps.UserService, _deps.PlayerService, _deps.CashgameService); } }
        public CashgameChart CashgameChart { get { return new CashgameChart(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService); } }
        public Matrix Matrix { get { return new Matrix(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService, _deps.EventService); } }
        public RunningCashgame RunningCashgame { get { return new RunningCashgame(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService, _deps.LocationService); } }
        public EditCashgameForm EditCashgameForm { get { return new EditCashgameForm(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationService); } }
        public EditCashgame EditCashgame { get { return new EditCashgame(_deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationService); } }
        public DeleteCashgame DeleteCashgame { get { return new DeleteCashgame(_deps.CashgameService, _deps.BunchService, _deps.UserService, _deps.PlayerService); } }
        public DeleteCheckpoint DeleteCheckpoint { get { return new DeleteCheckpoint(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService); } }
        public Buyin Buyin { get { return new Buyin(_deps.BunchService, _deps.PlayerService, _deps.CashgameService, _deps.UserService); } }
        public Report Report { get { return new Report(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService); } }
        public Cashout Cashout { get { return new Cashout(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService); } }
        public EndCashgame EndCashgame { get { return new EndCashgame(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService); } }
        
        // Player
        public PlayerList PlayerList { get { return new PlayerList(_deps.BunchService, _deps.UserService, _deps.PlayerService); } }
        public PlayerDetails PlayerDetails { get { return new PlayerDetails(_deps.BunchService, _deps.PlayerService, _deps.CashgameService, _deps.UserService); } }
        public PlayerFacts PlayerFacts { get { return new PlayerFacts(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService); } }
        public PlayerBadges PlayerBadges { get { return new PlayerBadges(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService); } }
        public InvitePlayer InvitePlayer { get { return new InvitePlayer(_deps.BunchService, _deps.PlayerService, _deps.MessageSender, _deps.UserService); } }
        public InvitePlayerForm InvitePlayerForm { get { return new InvitePlayerForm(_deps.BunchService, _deps.PlayerService, _deps.UserService); } }
        public InvitePlayerConfirmation InvitePlayerConfirmation { get { return new InvitePlayerConfirmation(_deps.BunchService, _deps.PlayerService, _deps.UserService); } }
        public AddPlayer AddPlayer { get { return new AddPlayer(_deps.BunchService, _deps.PlayerService, _deps.UserService); } }
        public DeletePlayer DeletePlayer { get { return new DeletePlayer(_deps.PlayerService, _deps.CashgameService, _deps.UserService, _deps.BunchService); } }

        // Apps
        public AppDetails AppDetails { get { return new AppDetails(_deps.AppService); } }
        public VerifyAppKey VerifyAppKey { get { return new VerifyAppKey(_deps.AppService); } }
        public AppList AppList { get { return new AppList(_deps.AppService, _deps.UserService); } }
        public AddApp AddApp { get { return new AddApp(_deps.AppService, _deps.UserService); } }
    }
}