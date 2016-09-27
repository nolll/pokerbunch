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
        public BaseContext BaseContext => new BaseContext();
        public CoreContext CoreContext => new CoreContext(_deps.UserService);
        public BunchContext BunchContext => new BunchContext(_deps.UserService, _deps.BunchService);
        public CashgameContext CashgameContext => new CashgameContext(_deps.UserService, _deps.BunchService, _deps.CashgameService);

        // Auth and Home
        public LoginForm LoginForm => new LoginForm();
        public Login Login => new Login(_deps.UserService, _deps.AuthService);

        // Admin
        public TestEmail TestEmail => new TestEmail(_deps.MessageSender, _deps.UserService);

        // User
        public UserList UserList => new UserList(_deps.UserService);
        public UserDetails UserDetails => new UserDetails(_deps.UserService);
        public AddUser AddUser => new AddUser(_deps.UserService, _deps.RandomService, _deps.MessageSender);
        public EditUserForm EditUserForm => new EditUserForm(_deps.UserService);
        public EditUser EditUser => new EditUser(_deps.UserService);
        public ForgotPassword ForgotPassword => new ForgotPassword(_deps.UserService, _deps.MessageSender, _deps.RandomService);
        public ChangePassword ChangePassword => new ChangePassword(_deps.UserService, _deps.RandomService);

        // Bunch
        public BunchList BunchList => new BunchList(_deps.BunchService, _deps.UserService);
        public BunchDetails BunchDetails => new BunchDetails(_deps.BunchService, _deps.UserService, _deps.PlayerService);
        public AddBunchForm AddBunchForm => new AddBunchForm();
        public AddBunch AddBunch => new AddBunch(_deps.UserService, _deps.BunchService, _deps.PlayerService);
        public EditBunchForm EditBunchForm => new EditBunchForm(_deps.BunchService, _deps.UserService, _deps.PlayerService);
        public EditBunch EditBunch => new EditBunch(_deps.BunchService, _deps.UserService, _deps.PlayerService);
        public JoinBunchForm JoinBunchForm => new JoinBunchForm(_deps.BunchService);
        public JoinBunch JoinBunch => new JoinBunch(_deps.BunchService, _deps.PlayerService, _deps.UserService);
        public JoinBunchConfirmation JoinBunchConfirmation => new JoinBunchConfirmation(_deps.BunchService, _deps.UserService, _deps.PlayerService);

        // Events
        public EventList EventList => new EventList(_deps.BunchService, _deps.EventService, _deps.UserService, _deps.PlayerService, _deps.LocationService);
        public EventDetails EventDetails => new EventDetails(_deps.EventService, _deps.UserService, _deps.PlayerService, _deps.BunchService);
        public AddEvent AddEvent => new AddEvent(_deps.BunchService, _deps.PlayerService, _deps.UserService, _deps.EventService);

        // Locations
        public LocationList LocationList => new LocationList(_deps.BunchService, _deps.UserService, _deps.PlayerService, _deps.LocationService);
        public LocationDetails LocationDetails => new LocationDetails(_deps.LocationService, _deps.UserService, _deps.PlayerService, _deps.BunchService);
        public AddLocation AddLocation => new AddLocation(_deps.BunchService, _deps.PlayerService, _deps.UserService, _deps.LocationService);

        // Cashgame
        public CashgameStatus CashgameStatus => new CashgameStatus(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService);
        public TopList TopList => new TopList(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public CurrentRankings CurrentRankings => new CurrentRankings(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public CashgameDetails CashgameDetails => new CashgameDetails(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationService);
        public CashgameDetailsChart CashgameDetailsChart => new CashgameDetailsChart(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public CashgameFacts CashgameFacts => new CashgameFacts(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public CashgameList CashgameList => new CashgameList(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationService);
        public AddCashgameForm AddCashgameForm => new AddCashgameForm(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationService, _deps.EventService);
        public AddCashgame AddCashgame => new AddCashgame(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationService, _deps.EventService);
        public Actions Actions => new Actions(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public ActionsChart ActionsChart => new ActionsChart(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService);
        public EditCheckpointForm EditCheckpointForm => new EditCheckpointForm(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService);
        public EditCheckpoint EditCheckpoint => new EditCheckpoint(_deps.BunchService, _deps.UserService, _deps.PlayerService, _deps.CashgameService);
        public CashgameChart CashgameChart => new CashgameChart(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public Matrix Matrix => new Matrix(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService, _deps.EventService);
        public RunningCashgame RunningCashgame => new RunningCashgame(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService, _deps.LocationService);
        public EditCashgameForm EditCashgameForm => new EditCashgameForm(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationService, _deps.EventService);
        public EditCashgame EditCashgame => new EditCashgame(_deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationService, _deps.EventService, _deps.BunchService);
        public DeleteCashgame DeleteCashgame => new DeleteCashgame(_deps.CashgameService, _deps.BunchService, _deps.UserService, _deps.PlayerService);
        public DeleteCheckpoint DeleteCheckpoint => new DeleteCheckpoint(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService);
        public Buyin Buyin => new Buyin(_deps.BunchService, _deps.PlayerService, _deps.CashgameService, _deps.UserService);
        public Report Report => new Report(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public Cashout Cashout => new Cashout(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public EndCashgame EndCashgame => new EndCashgame(_deps.BunchService, _deps.CashgameService, _deps.UserService, _deps.PlayerService);

        // Player
        public PlayerList PlayerList => new PlayerList(_deps.BunchService, _deps.UserService, _deps.PlayerService);
        public PlayerDetails PlayerDetails => new PlayerDetails(_deps.BunchService, _deps.PlayerService, _deps.CashgameService, _deps.UserService);
        public PlayerFacts PlayerFacts => new PlayerFacts(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public PlayerBadges PlayerBadges => new PlayerBadges(_deps.BunchService, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public InvitePlayer InvitePlayer => new InvitePlayer(_deps.BunchService, _deps.PlayerService, _deps.MessageSender, _deps.UserService);
        public InvitePlayerForm InvitePlayerForm => new InvitePlayerForm(_deps.BunchService, _deps.PlayerService, _deps.UserService);
        public InvitePlayerConfirmation InvitePlayerConfirmation => new InvitePlayerConfirmation(_deps.BunchService, _deps.PlayerService, _deps.UserService);
        public AddPlayer AddPlayer => new AddPlayer(_deps.BunchService, _deps.PlayerService, _deps.UserService);
        public DeletePlayer DeletePlayer => new DeletePlayer(_deps.PlayerService, _deps.CashgameService, _deps.UserService, _deps.BunchService);

        // Apps
        public AppDetails AppDetails => new AppDetails(_deps.AppService);
        public VerifyAppKey VerifyAppKey => new VerifyAppKey(_deps.AppService);
        public AppList AppList => new AppList(_deps.AppService, _deps.UserService);
        public AddApp AddApp => new AddApp(_deps.AppService, _deps.UserService);
    }
}