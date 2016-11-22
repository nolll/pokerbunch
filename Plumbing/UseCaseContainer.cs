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
        public BunchContext BunchContext => new BunchContext(_deps.UserRepository, _deps.BunchRepository);
        public CashgameContext CashgameContext => new CashgameContext(_deps.UserRepository, _deps.BunchRepository, _deps.CashgameRepository);

        // Auth and Home
        public LoginForm LoginForm => new LoginForm();
        public Login Login => new Login(_deps.UserRepository, _deps.TokenRepository);

        // Admin
        public TestEmail TestEmail => new TestEmail(_deps.MessageSender, _deps.UserRepository);
        public ClearCache ClearCache => new ClearCache(_deps.Cache);

        // User
        public UserList UserList => new UserList(_deps.UserRepository);
        public UserDetails UserDetails => new UserDetails(_deps.UserRepository);
        public AddUser AddUser => new AddUser(_deps.UserRepository, _deps.RandomService, _deps.MessageSender);
        public EditUserForm EditUserForm => new EditUserForm(_deps.UserRepository);
        public EditUser EditUser => new EditUser(_deps.UserRepository);
        public ForgotPassword ForgotPassword => new ForgotPassword(_deps.UserRepository, _deps.MessageSender, _deps.RandomService);
        public ChangePassword ChangePassword => new ChangePassword(_deps.UserRepository, _deps.RandomService);

        // Bunch
        public BunchList BunchList => new BunchList(_deps.BunchRepository, _deps.UserRepository);
        public BunchDetails BunchDetails => new BunchDetails(_deps.BunchRepository);
        public AddBunchForm AddBunchForm => new AddBunchForm();
        public AddBunch AddBunch => new AddBunch(_deps.BunchRepository);
        public EditBunchForm EditBunchForm => new EditBunchForm(_deps.BunchRepository, _deps.UserRepository, _deps.PlayerRepository);
        public EditBunch EditBunch => new EditBunch(_deps.BunchRepository);
        public JoinBunchForm JoinBunchForm => new JoinBunchForm(_deps.BunchRepository);
        public JoinBunch JoinBunch => new JoinBunch(_deps.BunchRepository, _deps.PlayerRepository, _deps.UserRepository);
        public JoinBunchConfirmation JoinBunchConfirmation => new JoinBunchConfirmation(_deps.BunchRepository, _deps.UserRepository, _deps.PlayerRepository);

        // Events
        public EventList EventList => new EventList(_deps.BunchRepository, _deps.EventRepository, _deps.UserRepository, _deps.PlayerRepository, _deps.LocationRepository);
        public EventDetails EventDetails => new EventDetails(_deps.EventRepository, _deps.UserRepository, _deps.PlayerRepository, _deps.BunchRepository);
        public AddEvent AddEvent => new AddEvent(_deps.BunchRepository, _deps.PlayerRepository, _deps.UserRepository, _deps.EventRepository);

        // Locations
        public LocationList LocationList => new LocationList(_deps.LocationRepository);
        public LocationDetails LocationDetails => new LocationDetails(_deps.LocationRepository);
        public AddLocation AddLocation => new AddLocation(_deps.LocationRepository);

        // Cashgame
        public CashgameStatus CashgameStatus => new CashgameStatus(_deps.BunchRepository, _deps.CashgameRepository, _deps.UserRepository, _deps.PlayerRepository);
        public TopList TopList => new TopList(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository);
        public CurrentRankings CurrentRankings => new CurrentRankings(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository);
        public CashgameDetails CashgameDetails => new CashgameDetails(_deps.BunchRepository, _deps.CashgameRepository, _deps.UserRepository, _deps.PlayerRepository, _deps.LocationRepository);
        public CashgameDetailsChart CashgameDetailsChart => new CashgameDetailsChart(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository);
        public CashgameFacts CashgameFacts => new CashgameFacts(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository);
        public CashgameList CashgameList => new CashgameList(_deps.BunchRepository, _deps.CashgameRepository, _deps.UserRepository, _deps.PlayerRepository, _deps.LocationRepository);
        public AddCashgameForm AddCashgameForm => new AddCashgameForm(_deps.BunchRepository, _deps.CashgameRepository, _deps.UserRepository, _deps.PlayerRepository, _deps.LocationRepository, _deps.EventRepository);
        public AddCashgame AddCashgame => new AddCashgame(_deps.BunchRepository, _deps.CashgameRepository, _deps.UserRepository, _deps.PlayerRepository, _deps.LocationRepository, _deps.EventRepository);
        public Actions Actions => new Actions(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository);
        public ActionsChart ActionsChart => new ActionsChart(_deps.BunchRepository, _deps.CashgameRepository, _deps.UserRepository, _deps.PlayerRepository);
        public EditCheckpointForm EditCheckpointForm => new EditCheckpointForm(_deps.BunchRepository, _deps.CashgameRepository, _deps.UserRepository, _deps.PlayerRepository);
        public EditCheckpoint EditCheckpoint => new EditCheckpoint(_deps.BunchRepository, _deps.UserRepository, _deps.PlayerRepository, _deps.CashgameRepository);
        public CashgameChart CashgameChart => new CashgameChart(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository);
        public Matrix Matrix => new Matrix(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository, _deps.EventRepository);
        public RunningCashgame RunningCashgame => new RunningCashgame(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository, _deps.LocationRepository);
        public EditCashgameForm EditCashgameForm => new EditCashgameForm(_deps.BunchRepository, _deps.CashgameRepository, _deps.UserRepository, _deps.PlayerRepository, _deps.LocationRepository, _deps.EventRepository);
        public EditCashgame EditCashgame => new EditCashgame(_deps.CashgameRepository, _deps.UserRepository, _deps.PlayerRepository, _deps.LocationRepository, _deps.EventRepository, _deps.BunchRepository);
        public DeleteCashgame DeleteCashgame => new DeleteCashgame(_deps.CashgameRepository, _deps.BunchRepository, _deps.UserRepository, _deps.PlayerRepository);
        public DeleteCheckpoint DeleteCheckpoint => new DeleteCheckpoint(_deps.BunchRepository, _deps.CashgameRepository, _deps.UserRepository, _deps.PlayerRepository);
        public Buyin Buyin => new Buyin(_deps.BunchRepository, _deps.PlayerRepository, _deps.CashgameRepository, _deps.UserRepository);
        public Report Report => new Report(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository);
        public Cashout Cashout => new Cashout(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository);
        public EndCashgame EndCashgame => new EndCashgame(_deps.BunchRepository, _deps.CashgameRepository, _deps.UserRepository, _deps.PlayerRepository);

        // Player
        public PlayerList PlayerList => new PlayerList(_deps.BunchRepository, _deps.UserRepository, _deps.PlayerRepository);
        public PlayerDetails PlayerDetails => new PlayerDetails(_deps.BunchRepository, _deps.PlayerRepository, _deps.CashgameRepository, _deps.UserRepository);
        public PlayerFacts PlayerFacts => new PlayerFacts(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository);
        public PlayerBadges PlayerBadges => new PlayerBadges(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository);
        public InvitePlayer InvitePlayer => new InvitePlayer(_deps.BunchRepository, _deps.PlayerRepository, _deps.MessageSender, _deps.UserRepository);
        public InvitePlayerForm InvitePlayerForm => new InvitePlayerForm(_deps.BunchRepository, _deps.PlayerRepository, _deps.UserRepository);
        public InvitePlayerConfirmation InvitePlayerConfirmation => new InvitePlayerConfirmation(_deps.BunchRepository, _deps.PlayerRepository, _deps.UserRepository);
        public AddPlayer AddPlayer => new AddPlayer(_deps.BunchRepository, _deps.PlayerRepository, _deps.UserRepository);
        public DeletePlayer DeletePlayer => new DeletePlayer(_deps.PlayerRepository, _deps.CashgameRepository, _deps.UserRepository, _deps.BunchRepository);

        // Apps
        public AppDetails AppDetails => new AppDetails(_deps.AppRepository);
        public AppListUser AppListUser => new AppListUser(_deps.AppRepository);
        public AppListAll AllAppsList => new AppListAll(_deps.AppRepository);
        public AddApp AddApp => new AddApp(_deps.AppRepository, _deps.UserRepository);
    }
}