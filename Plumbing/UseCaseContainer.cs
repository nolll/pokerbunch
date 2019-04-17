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
        public CoreContext CoreContext => new CoreContext(_deps.UserService);
        public BunchContext BunchContext => new BunchContext(_deps.BunchService);

        // Auth and Home
        public Login Login => new Login(_deps.AuthService, _deps.UserService);

        // Admin
        public TestEmail TestEmail => new TestEmail(_deps.AdminService);
        public ClearCache ClearCache => new ClearCache(_deps.AdminService);

        // User
        public UserDetails UserDetails => new UserDetails(_deps.UserService);
        public AddUser AddUser => new AddUser(_deps.UserService);
        public EditUserForm EditUserForm => new EditUserForm(_deps.UserService);
        public EditUser EditUser => new EditUser(_deps.UserService);
        public ForgotPassword ForgotPassword => new ForgotPassword(_deps.UserService);
        public ChangePassword ChangePassword => new ChangePassword(_deps.UserService);

        // Bunch
        public BunchList BunchList => new BunchList(_deps.BunchService);
        public AddBunchForm AddBunchForm => new AddBunchForm();
        public AddBunch AddBunch => new AddBunch(_deps.BunchService);
        public EditBunchForm EditBunchForm => new EditBunchForm(_deps.BunchService);
        public EditBunch EditBunch => new EditBunch(_deps.BunchService);
        public JoinBunchForm JoinBunchForm => new JoinBunchForm(_deps.BunchService);
        public JoinBunch JoinBunch => new JoinBunch(_deps.BunchService);
        public JoinBunchConfirmation JoinBunchConfirmation => new JoinBunchConfirmation(_deps.BunchService);

        // Events
        public EventList EventList => new EventList(_deps.EventService);
        public EventDetails EventDetails => new EventDetails(_deps.EventService);
        public AddEvent AddEvent => new AddEvent(_deps.EventService);

        // Locations
        public LocationList LocationList => new LocationList(_deps.LocationService);
        public LocationDetails LocationDetails => new LocationDetails(_deps.LocationService);
        public AddLocation AddLocation => new AddLocation(_deps.LocationService);

        // Cashgame
        public AddCashgameForm AddCashgameForm => new AddCashgameForm(_deps.BunchService, _deps.CashgameService, _deps.LocationService);
        public AddCashgame AddCashgame => new AddCashgame(_deps.CashgameService);
        public EventMatrix EventMatrix => new EventMatrix(_deps.BunchService, _deps.EventService, _deps.CashgameService, _deps.PlayerService);
        public EditCashgameForm EditCashgameForm => new EditCashgameForm(_deps.CashgameService, _deps.LocationService, _deps.EventService);
        public EditCashgame EditCashgame => new EditCashgame(_deps.CashgameService);
        public DeleteCashgame DeleteCashgame => new DeleteCashgame(_deps.CashgameService);

        // Player
        public PlayerDetails PlayerDetails => new PlayerDetails(_deps.BunchService, _deps.PlayerService, _deps.CashgameService, _deps.UserService);
        public PlayerFacts PlayerFacts => new PlayerFacts(_deps.BunchService, _deps.CashgameService, _deps.PlayerService);
        public PlayerBadges PlayerBadges => new PlayerBadges(_deps.CashgameService);
        public InvitePlayer InvitePlayer => new InvitePlayer(_deps.PlayerService);
        public InvitePlayerForm InvitePlayerForm => new InvitePlayerForm(_deps.BunchService, _deps.PlayerService);
        public InvitePlayerConfirmation InvitePlayerConfirmation => new InvitePlayerConfirmation(_deps.BunchService, _deps.PlayerService);
        public AddPlayer AddPlayer => new AddPlayer(_deps.PlayerService);
        public DeletePlayer DeletePlayer => new DeletePlayer(_deps.PlayerService, _deps.CashgameService);

        // Apps
        public AppDetails AppDetails => new AppDetails(_deps.AppService);
        public AppListUser AppListUser => new AppListUser(_deps.AppService);
        public AppListAll AllAppsList => new AppListAll(_deps.AppService);
        public AddApp AddApp => new AddApp(_deps.AppService);
        public DeleteApp DeleteApp => new DeleteApp(_deps.AppService);
    }
}