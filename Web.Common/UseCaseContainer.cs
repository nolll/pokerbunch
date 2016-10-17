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
        public BunchContext BunchContext => new BunchContext(_deps.UserService, _deps.BunchRepository);
        public CashgameContext CashgameContext => new CashgameContext(_deps.UserService, _deps.BunchRepository, _deps.CashgameService);

        // Auth and Home
        public LoginForm LoginForm => new LoginForm();
        public Login Login => new Login(_deps.UserService, _deps.AuthService);

        // Admin
        public TestEmail TestEmail => new TestEmail(_deps.MessageSender, _deps.UserService);
        public ClearCache ClearCache => new ClearCache(_deps.Cache);

        // User
        public UserList UserList => new UserList(_deps.UserService);
        public UserDetails UserDetails => new UserDetails(_deps.UserService);
        public AddUser AddUser => new AddUser(_deps.UserService, _deps.RandomService, _deps.MessageSender);
        public EditUserForm EditUserForm => new EditUserForm(_deps.UserService);
        public EditUser EditUser => new EditUser(_deps.UserService);
        public ForgotPassword ForgotPassword => new ForgotPassword(_deps.UserService, _deps.MessageSender, _deps.RandomService);
        public ChangePassword ChangePassword => new ChangePassword(_deps.UserService, _deps.RandomService);

        // Bunch
        public BunchList BunchList => new BunchList(_deps.BunchRepository, _deps.UserService);
        public BunchDetails BunchDetails => new BunchDetails(_deps.BunchRepository);
        public AddBunchForm AddBunchForm => new AddBunchForm();
        public AddBunch AddBunch => new AddBunch(_deps.BunchRepository);
        public EditBunchForm EditBunchForm => new EditBunchForm(_deps.BunchRepository, _deps.UserService, _deps.PlayerService);
        public EditBunch EditBunch => new EditBunch(_deps.BunchRepository);
        public JoinBunchForm JoinBunchForm => new JoinBunchForm(_deps.BunchRepository);
        public JoinBunch JoinBunch => new JoinBunch(_deps.BunchRepository, _deps.PlayerService, _deps.UserService);
        public JoinBunchConfirmation JoinBunchConfirmation => new JoinBunchConfirmation(_deps.BunchRepository, _deps.UserService, _deps.PlayerService);

        // Events
        public EventList EventList => new EventList(_deps.BunchRepository, _deps.EventService, _deps.UserService, _deps.PlayerService, _deps.LocationRepository);
        public EventDetails EventDetails => new EventDetails(_deps.EventService, _deps.UserService, _deps.PlayerService, _deps.BunchRepository);
        public AddEvent AddEvent => new AddEvent(_deps.BunchRepository, _deps.PlayerService, _deps.UserService, _deps.EventService);

        // Locations
        public LocationList LocationList => new LocationList(_deps.LocationRepository);
        public LocationDetails LocationDetails => new LocationDetails(_deps.LocationRepository);
        public AddLocation AddLocation => new AddLocation(_deps.LocationRepository);

        // Cashgame
        public CashgameStatus CashgameStatus => new CashgameStatus(_deps.BunchRepository, _deps.CashgameService, _deps.UserService, _deps.PlayerService);
        public TopList TopList => new TopList(_deps.BunchRepository, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public CurrentRankings CurrentRankings => new CurrentRankings(_deps.BunchRepository, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public CashgameDetails CashgameDetails => new CashgameDetails(_deps.BunchRepository, _deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationRepository);
        public CashgameDetailsChart CashgameDetailsChart => new CashgameDetailsChart(_deps.BunchRepository, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public CashgameFacts CashgameFacts => new CashgameFacts(_deps.BunchRepository, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public CashgameList CashgameList => new CashgameList(_deps.BunchRepository, _deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationRepository);
        public AddCashgameForm AddCashgameForm => new AddCashgameForm(_deps.BunchRepository, _deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationRepository, _deps.EventService);
        public AddCashgame AddCashgame => new AddCashgame(_deps.BunchRepository, _deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationRepository, _deps.EventService);
        public Actions Actions => new Actions(_deps.BunchRepository, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public ActionsChart ActionsChart => new ActionsChart(_deps.BunchRepository, _deps.CashgameService, _deps.UserService, _deps.PlayerService);
        public EditCheckpointForm EditCheckpointForm => new EditCheckpointForm(_deps.BunchRepository, _deps.CashgameService, _deps.UserService, _deps.PlayerService);
        public EditCheckpoint EditCheckpoint => new EditCheckpoint(_deps.BunchRepository, _deps.UserService, _deps.PlayerService, _deps.CashgameService);
        public CashgameChart CashgameChart => new CashgameChart(_deps.BunchRepository, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public Matrix Matrix => new Matrix(_deps.BunchRepository, _deps.CashgameService, _deps.PlayerService, _deps.UserService, _deps.EventService);
        public RunningCashgame RunningCashgame => new RunningCashgame(_deps.BunchRepository, _deps.CashgameService, _deps.PlayerService, _deps.UserService, _deps.LocationRepository);
        public EditCashgameForm EditCashgameForm => new EditCashgameForm(_deps.BunchRepository, _deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationRepository, _deps.EventService);
        public EditCashgame EditCashgame => new EditCashgame(_deps.CashgameService, _deps.UserService, _deps.PlayerService, _deps.LocationRepository, _deps.EventService, _deps.BunchRepository);
        public DeleteCashgame DeleteCashgame => new DeleteCashgame(_deps.CashgameService, _deps.BunchRepository, _deps.UserService, _deps.PlayerService);
        public DeleteCheckpoint DeleteCheckpoint => new DeleteCheckpoint(_deps.BunchRepository, _deps.CashgameService, _deps.UserService, _deps.PlayerService);
        public Buyin Buyin => new Buyin(_deps.BunchRepository, _deps.PlayerService, _deps.CashgameService, _deps.UserService);
        public Report Report => new Report(_deps.BunchRepository, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public Cashout Cashout => new Cashout(_deps.BunchRepository, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public EndCashgame EndCashgame => new EndCashgame(_deps.BunchRepository, _deps.CashgameService, _deps.UserService, _deps.PlayerService);

        // Player
        public PlayerList PlayerList => new PlayerList(_deps.BunchRepository, _deps.UserService, _deps.PlayerService);
        public PlayerDetails PlayerDetails => new PlayerDetails(_deps.BunchRepository, _deps.PlayerService, _deps.CashgameService, _deps.UserService);
        public PlayerFacts PlayerFacts => new PlayerFacts(_deps.BunchRepository, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public PlayerBadges PlayerBadges => new PlayerBadges(_deps.BunchRepository, _deps.CashgameService, _deps.PlayerService, _deps.UserService);
        public InvitePlayer InvitePlayer => new InvitePlayer(_deps.BunchRepository, _deps.PlayerService, _deps.MessageSender, _deps.UserService);
        public InvitePlayerForm InvitePlayerForm => new InvitePlayerForm(_deps.BunchRepository, _deps.PlayerService, _deps.UserService);
        public InvitePlayerConfirmation InvitePlayerConfirmation => new InvitePlayerConfirmation(_deps.BunchRepository, _deps.PlayerService, _deps.UserService);
        public AddPlayer AddPlayer => new AddPlayer(_deps.BunchRepository, _deps.PlayerService, _deps.UserService);
        public DeletePlayer DeletePlayer => new DeletePlayer(_deps.PlayerService, _deps.CashgameService, _deps.UserService, _deps.BunchRepository);

        // Apps
        public AppDetails AppDetails => new AppDetails(_deps.AppService);
        public VerifyAppKey VerifyAppKey => new VerifyAppKey(_deps.AppService);
        public AppList AppList => new AppList(_deps.AppService, _deps.UserService);
        public AddApp AddApp => new AddApp(_deps.AppService, _deps.UserService);
    }
}