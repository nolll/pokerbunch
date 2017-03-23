using Core.UseCases;

namespace Plumbing
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
        public CoreContext CoreContext => new CoreContext(_deps.UserRepository);
        public BunchContext BunchContext => new BunchContext(_deps.BunchRepository);
        public CashgameContext CashgameContext => new CashgameContext(_deps.CashgameRepository);

        // Auth and Home
        public LoginForm LoginForm => new LoginForm();
        public Login Login => new Login(_deps.UserRepository, _deps.TokenRepository);

        // Admin
        public TestEmail TestEmail => new TestEmail(_deps.AdminService);
        public ClearCache ClearCache => new ClearCache(_deps.AdminService);

        // User
        public UserList UserList => new UserList(_deps.UserRepository);
        public UserDetails UserDetails => new UserDetails(_deps.UserRepository);
        public AddUser AddUser => new AddUser(_deps.UserRepository);
        public EditUserForm EditUserForm => new EditUserForm(_deps.UserRepository);
        public EditUser EditUser => new EditUser(_deps.UserRepository);
        public ForgotPassword ForgotPassword => new ForgotPassword(_deps.UserRepository);
        public ChangePassword ChangePassword => new ChangePassword(_deps.UserRepository);

        // Bunch
        public BunchList BunchList => new BunchList(_deps.BunchRepository, _deps.UserRepository);
        public BunchDetails BunchDetails => new BunchDetails(_deps.BunchRepository);
        public AddBunchForm AddBunchForm => new AddBunchForm();
        public AddBunch AddBunch => new AddBunch(_deps.BunchRepository);
        public EditBunchForm EditBunchForm => new EditBunchForm(_deps.BunchRepository);
        public EditBunch EditBunch => new EditBunch(_deps.BunchRepository);
        public JoinBunchForm JoinBunchForm => new JoinBunchForm(_deps.BunchRepository);
        public JoinBunch JoinBunch => new JoinBunch(_deps.BunchRepository);
        public JoinBunchConfirmation JoinBunchConfirmation => new JoinBunchConfirmation(_deps.BunchRepository);

        // Events
        public EventList EventList => new EventList(_deps.EventRepository);
        public EventDetails EventDetails => new EventDetails(_deps.EventRepository);
        public AddEvent AddEvent => new AddEvent(_deps.EventRepository);

        // Locations
        public LocationList LocationList => new LocationList(_deps.LocationRepository);
        public LocationDetails LocationDetails => new LocationDetails(_deps.LocationRepository);
        public AddLocation AddLocation => new AddLocation(_deps.LocationRepository);

        // Cashgame
        public CashgameStatus CashgameStatus => new CashgameStatus(_deps.BunchRepository, _deps.CashgameRepository, _deps.UserRepository, _deps.PlayerRepository);
        public TopList TopList => new TopList(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository);
        public CurrentRankings CurrentRankings => new CurrentRankings(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository);
        public CashgameDetails CashgameDetails => new CashgameDetails(_deps.CashgameRepository);
        public CashgameDetailsChart CashgameDetailsChart => new CashgameDetailsChart(_deps.CashgameRepository);
        public CashgameFacts CashgameFacts => new CashgameFacts(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository);
        public CashgameList CashgameList => new CashgameList(_deps.BunchRepository, _deps.CashgameRepository);
        public AddCashgameForm AddCashgameForm => new AddCashgameForm(_deps.BunchRepository, _deps.CashgameRepository, _deps.LocationRepository, _deps.EventRepository);
        public AddCashgame AddCashgame => new AddCashgame(_deps.BunchRepository, _deps.CashgameRepository, _deps.EventRepository);
        public Actions Actions => new Actions(_deps.CashgameRepository);
        public ActionsChart ActionsChart => new ActionsChart(_deps.CashgameRepository);
        public EditCheckpointForm EditCheckpointForm => new EditCheckpointForm(_deps.CashgameRepository);
        public EditCheckpoint EditCheckpoint => new EditCheckpoint(_deps.CashgameRepository);
        public CashgameChart CashgameChart => new CashgameChart(_deps.CashgameRepository, _deps.PlayerRepository);
        public BunchMatrix BunchMatrix => new BunchMatrix(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository);
        public EventMatrix EventMatrix => new EventMatrix(_deps.BunchRepository, _deps.EventRepository, _deps.CashgameRepository, _deps.PlayerRepository);
        public RunningCashgame RunningCashgame => new RunningCashgame(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository);
        public EditCashgameForm EditCashgameForm => new EditCashgameForm(_deps.CashgameRepository, _deps.LocationRepository, _deps.EventRepository);
        public EditCashgame EditCashgame => new EditCashgame(_deps.CashgameRepository);
        public DeleteCashgame DeleteCashgame => new DeleteCashgame(_deps.CashgameRepository);
        public DeleteCheckpoint DeleteCheckpoint => new DeleteCheckpoint(_deps.BunchRepository, _deps.CashgameRepository);
        public Buyin Buyin => new Buyin(_deps.CashgameRepository);
        public Report Report => new Report(_deps.CashgameRepository);
        public Cashout Cashout => new Cashout(_deps.CashgameRepository);
        public EndCashgame EndCashgame => new EndCashgame(_deps.CashgameRepository);

        // Player
        public PlayerList PlayerList => new PlayerList(_deps.BunchRepository, _deps.PlayerRepository);
        public PlayerDetails PlayerDetails => new PlayerDetails(_deps.BunchRepository, _deps.PlayerRepository, _deps.CashgameRepository, _deps.UserRepository);
        public PlayerFacts PlayerFacts => new PlayerFacts(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository);
        public PlayerBadges PlayerBadges => new PlayerBadges(_deps.CashgameRepository);
        public InvitePlayer InvitePlayer => new InvitePlayer(_deps.PlayerRepository);
        public InvitePlayerForm InvitePlayerForm => new InvitePlayerForm(_deps.BunchRepository, _deps.PlayerRepository, _deps.UserRepository);
        public InvitePlayerConfirmation InvitePlayerConfirmation => new InvitePlayerConfirmation(_deps.BunchRepository, _deps.PlayerRepository, _deps.UserRepository);
        public AddPlayer AddPlayer => new AddPlayer(_deps.PlayerRepository);
        public DeletePlayer DeletePlayer => new DeletePlayer(_deps.PlayerRepository, _deps.CashgameRepository);

        // Apps
        public AppDetails AppDetails => new AppDetails(_deps.AppRepository);
        public AppListUser AppListUser => new AppListUser(_deps.AppRepository);
        public AppListAll AllAppsList => new AppListAll(_deps.AppRepository);
        public AddApp AddApp => new AddApp(_deps.AppRepository);
    }
}