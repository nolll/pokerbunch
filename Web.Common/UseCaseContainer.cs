using Core.Exceptions;
using Core.Repositories;
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
        public CashgameContext CashgameContext { get { return new CashgameContext(_deps.UserService, _deps.BunchService, _deps.CashgameRepository); } }

        // Auth and Home
        public LoginForm LoginForm { get { return new LoginForm(); } }
        public Login Login { get { return new Login(_deps.UserService); } }

        // Admin
        public TestEmail TestEmail { get { return new TestEmail(_deps.MessageSender, _deps.UserService); } }

        // User
        public UserList UserList { get { return new UserList(_deps.UserRepository); } }
        public UserDetails UserDetails { get { return new UserDetails(_deps.UserService); } }
        public AddUser AddUser { get { return new AddUser(_deps.UserService, _deps.RandomService, _deps.MessageSender); } }
        public EditUserForm EditUserForm { get { return new EditUserForm(_deps.UserService); } }
        public EditUser EditUser { get { return new EditUser(_deps.UserService); } }
        public ForgotPassword ForgotPassword { get { return new ForgotPassword(_deps.UserService, _deps.MessageSender, _deps.RandomService); } }
        public ChangePassword ChangePassword { get { return new ChangePassword(_deps.UserService, _deps.RandomService); } } 

        // Bunch
        public BunchList BunchList { get { return new BunchList(_deps.BunchService, _deps.UserService); } }
        public BunchDetails BunchDetails { get { return new BunchDetails(_deps.BunchService, _deps.UserService, _deps.PlayerRepository); } }
        public AddBunchForm AddBunchForm { get { return new AddBunchForm(); } }
        public AddBunch AddBunch { get { return new AddBunch(_deps.UserService, _deps.BunchService, _deps.PlayerRepository); } }
        public EditBunchForm EditBunchForm { get { return new EditBunchForm(_deps.BunchService, _deps.UserService, _deps.PlayerRepository); } }
        public EditBunch EditBunch { get { return new EditBunch(_deps.BunchService, _deps.UserService, _deps.PlayerRepository); } }
        public JoinBunchForm JoinBunchForm { get { return new JoinBunchForm(_deps.BunchService); } }
        public JoinBunch JoinBunch { get { return new JoinBunch(_deps.BunchService, _deps.PlayerRepository, _deps.UserService); } }
        public JoinBunchConfirmation JoinBunchConfirmation { get { return new JoinBunchConfirmation(_deps.BunchService, _deps.UserService, _deps.PlayerRepository); } }

        // Events
        public EventList EventList { get { return new EventList(_deps.BunchService, _deps.EventRepository, _deps.UserService, _deps.PlayerRepository); } }
        public EventDetails EventDetails { get { return new EventDetails(_deps.EventRepository, _deps.UserService, _deps.PlayerRepository, _deps.BunchService); } } 

        // Cashgame
        public CashgameStatus CashgameStatus { get { return new CashgameStatus(_deps.BunchService, _deps.CashgameRepository, _deps.UserService, _deps.PlayerRepository); } }
        public TopList TopList { get { return new TopList(_deps.BunchService, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserService); } }
        public CurrentRankings CurrentRankings { get { return new CurrentRankings(_deps.BunchService, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserService); } }
        public CashgameDetails CashgameDetails { get { return new CashgameDetails(_deps.BunchService, _deps.CashgameRepository, _deps.UserService, _deps.PlayerRepository); } }
        public CashgameDetailsChart CashgameDetailsChart { get { return new CashgameDetailsChart(_deps.BunchService, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserService); } }
        public CashgameFacts CashgameFacts { get { return new CashgameFacts(_deps.BunchService, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserService); } }
        public CashgameList CashgameList { get { return new CashgameList(_deps.BunchService, _deps.CashgameRepository, _deps.UserService, _deps.PlayerRepository); } }
        public AddCashgameForm AddCashgameForm { get { return new AddCashgameForm(_deps.BunchService, _deps.CashgameRepository, _deps.UserService, _deps.PlayerRepository); } }
        public AddCashgame AddCashgame { get { return new AddCashgame(_deps.BunchService, _deps.CashgameRepository, _deps.UserService, _deps.PlayerRepository); } }
        public Actions Actions { get { return new Actions(_deps.BunchService, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserService); } }
        public ActionsChart ActionsChart { get { return new ActionsChart(_deps.BunchService, _deps.CashgameRepository, _deps.UserService, _deps.PlayerRepository); } }
        public EditCheckpointForm EditCheckpointForm { get { return new EditCheckpointForm(_deps.BunchService, _deps.CheckpointRepository, _deps.CashgameRepository, _deps.UserService, _deps.PlayerRepository); } }
        public EditCheckpoint EditCheckpoint { get { return new EditCheckpoint(_deps.BunchService, _deps.CheckpointRepository, _deps.UserService, _deps.PlayerRepository, _deps.CashgameRepository); } }
        public CashgameChart CashgameChart { get { return new CashgameChart(_deps.BunchService, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserService); } }
        public Matrix Matrix { get { return new Matrix(_deps.BunchService, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserService, _deps.EventRepository); } }
        public RunningCashgame RunningCashgame { get { return new RunningCashgame(_deps.BunchService, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserService); } }
        public EditCashgameForm EditCashgameForm { get { return new EditCashgameForm(_deps.BunchService, _deps.CashgameRepository, _deps.UserService, _deps.PlayerRepository); } }
        public EditCashgame EditCashgame { get { return new EditCashgame(_deps.CashgameRepository, _deps.UserService, _deps.PlayerRepository); } }
        public DeleteCashgame DeleteCashgame { get { return new DeleteCashgame(_deps.CashgameRepository, _deps.BunchService, _deps.UserService, _deps.PlayerRepository); } }
        public DeleteCheckpoint DeleteCheckpoint { get { return new DeleteCheckpoint(_deps.BunchService, _deps.CashgameRepository, _deps.CheckpointRepository, _deps.UserService, _deps.PlayerRepository); }}
        public Buyin Buyin { get { return new Buyin(_deps.BunchService, _deps.PlayerRepository, _deps.CashgameRepository, _deps.CheckpointRepository, _deps.UserService); } }
        public Report Report { get { return new Report(_deps.BunchService, _deps.CashgameRepository, _deps.PlayerRepository, _deps.CheckpointRepository, _deps.UserService); } }
        public Cashout Cashout { get { return new Cashout(_deps.BunchService, _deps.CashgameRepository, _deps.PlayerRepository, _deps.CheckpointRepository, _deps.UserService); } }
        public EndCashgame EndCashgame { get { return new EndCashgame(_deps.BunchService, _deps.CashgameRepository, _deps.UserService, _deps.PlayerRepository); } }
        
        // Player
        public PlayerList PlayerList { get { return new PlayerList(_deps.BunchService, _deps.UserService, _deps.PlayerRepository); } }
        public PlayerDetails PlayerDetails { get { return new PlayerDetails(_deps.BunchService, _deps.PlayerRepository, _deps.CashgameRepository, _deps.UserService); } }
        public PlayerFacts PlayerFacts { get { return new PlayerFacts(_deps.BunchService, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserService); } }
        public PlayerBadges PlayerBadges { get { return new PlayerBadges(_deps.BunchService, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserService); } }
        public InvitePlayer InvitePlayer { get { return new InvitePlayer(_deps.BunchService, _deps.PlayerRepository, _deps.MessageSender, _deps.UserService); } }
        public InvitePlayerForm InvitePlayerForm { get { return new InvitePlayerForm(_deps.BunchService, _deps.PlayerRepository, _deps.UserService); } }
        public InvitePlayerConfirmation InvitePlayerConfirmation { get { return new InvitePlayerConfirmation(_deps.BunchService, _deps.PlayerRepository, _deps.UserService); } }
        public AddPlayer AddPlayer { get { return new AddPlayer(_deps.BunchService, _deps.PlayerRepository, _deps.UserService); } }
        public DeletePlayer DeletePlayer { get { return new DeletePlayer(_deps.PlayerRepository, _deps.CashgameRepository, _deps.UserService, _deps.BunchService); } }

        // Apps
        public AppDetails AppDetails { get { return new AppDetails(_deps.AppRepository); } }
        public VerifyAppKey VerifyAppKey { get { return new VerifyAppKey(_deps.AppRepository); } }
        public AppList AppList { get { return new AppList(_deps.AppRepository, _deps.UserService); } }
        public AddApp AddApp { get { return new AddApp(_deps.AppRepository, _deps.UserService); } }
    }
}