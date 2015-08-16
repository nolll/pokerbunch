using Core.UseCases;
using Plumbing;

namespace Web.Common
{
    public class UseCaseContainer
    {
        private readonly Dependencies _deps = new Dependencies();

        // Contexts
        public BaseContext BaseContext { get { return new BaseContext(); } }
        public AppContext AppContext { get { return new AppContext(_deps.UserRepository); } }
        public BunchContext BunchContext { get { return new BunchContext(_deps.UserRepository, _deps.BunchRepository, _deps.PlayerRepository, _deps.CashgameRepository); } }
        public CashgameContext CashgameContext { get { return new CashgameContext(_deps.UserRepository, _deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }

        // Auth and Home
        public LoginForm LoginForm { get { return new LoginForm(); } }
        public Login Login { get { return new Login(_deps.UserRepository); } }

        // Admin
        public TestEmail TestEmail { get { return new TestEmail(_deps.MessageSender, _deps.UserRepository); } }
        public ClearCache ClearCache { get { return new ClearCache(_deps.CacheContainer, _deps.UserRepository); } }

        // User
        public UserList UserList { get { return new UserList(_deps.UserRepository); } }
        public UserDetails UserDetails { get { return new UserDetails(_deps.UserRepository); } }
        public AddUser AddUser { get { return new AddUser(_deps.UserRepository, _deps.RandomService, _deps.MessageSender); } }
        public EditUserForm EditUserForm { get { return new EditUserForm(_deps.UserRepository); } }
        public EditUser EditUser { get { return new EditUser(_deps.UserRepository); } }
        public ForgotPassword ForgotPassword { get { return new ForgotPassword(_deps.UserRepository, _deps.MessageSender, _deps.RandomService); } }
        public ChangePassword ChangePassword { get { return new ChangePassword(_deps.UserRepository, _deps.RandomService); } } 

        // Bunch
        public BunchList BunchList { get { return new BunchList(_deps.BunchRepository, _deps.UserRepository); } }
        public BunchDetails BunchDetails { get { return new BunchDetails(_deps.BunchRepository, _deps.UserRepository, _deps.PlayerRepository); } }
        public AddBunchForm AddBunchForm { get { return new AddBunchForm(); } }
        public AddBunch AddBunch { get { return new AddBunch(_deps.UserRepository, _deps.BunchRepository, _deps.PlayerRepository); } }
        public EditBunchForm EditBunchForm { get { return new EditBunchForm(_deps.BunchRepository); } }
        public EditBunch EditBunch { get { return new EditBunch(_deps.BunchRepository); } }
        public JoinBunchForm JoinBunchForm { get { return new JoinBunchForm(_deps.BunchRepository); } }
        public JoinBunch JoinBunch { get { return new JoinBunch(_deps.BunchRepository, _deps.PlayerRepository, _deps.UserRepository); } }
        public JoinBunchConfirmation JoinBunchConfirmation { get { return new JoinBunchConfirmation(_deps.BunchRepository); } }

        // Events
        public EventList EventList { get { return new EventList(_deps.BunchRepository, _deps.EventRepository); } }
        public EventDetails EventDetails { get { return new EventDetails(_deps.EventRepository); } } 

        // Cashgame
        public CashgameStatus CashgameStatus { get { return new CashgameStatus(_deps.BunchRepository, _deps.CashgameRepository); } }
        public TopList TopList { get { return new TopList(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
        public CurrentRankings CurrentRankings { get { return new CurrentRankings(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
        public CashgameDetails CashgameDetails { get { return new CashgameDetails(_deps.BunchRepository, _deps.CashgameRepository, _deps.UserRepository, _deps.PlayerRepository); } }
        public CashgameDetailsChart CashgameDetailsChart { get { return new CashgameDetailsChart(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository); } }
        public CashgameFacts CashgameFacts { get { return new CashgameFacts(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
        public CashgameList CashgameList { get { return new CashgameList(_deps.BunchRepository, _deps.CashgameRepository); } }
        public AddCashgameForm AddCashgameForm { get { return new AddCashgameForm(_deps.BunchRepository, _deps.CashgameRepository); } }
        public AddCashgame AddCashgame { get { return new AddCashgame(_deps.BunchRepository, _deps.CashgameRepository); } }
        public Actions Actions { get { return new Actions(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository); } }
        public ActionsChart ActionsChart { get { return new ActionsChart(_deps.BunchRepository, _deps.CashgameRepository); } }
        public EditCheckpointForm EditCheckpointForm { get { return new EditCheckpointForm(_deps.BunchRepository, _deps.CheckpointRepository); } }
        public EditCheckpoint EditCheckpoint { get { return new EditCheckpoint(_deps.BunchRepository, _deps.CheckpointRepository); } }
        public CashgameChart CashgameChart { get { return new CashgameChart(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
        public Matrix Matrix { get { return new Matrix(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
        public RunningCashgame RunningCashgame { get { return new RunningCashgame(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository); } }
        public EditCashgameForm EditCashgameForm { get { return new EditCashgameForm(_deps.BunchRepository, _deps.CashgameRepository); } }
        public EditCashgame EditCashgame { get { return new EditCashgame(_deps.BunchRepository, _deps.CashgameRepository); } }
        public DeleteCashgame DeleteCashgame { get { return new DeleteCashgame(_deps.CashgameRepository, _deps.BunchRepository, _deps.UserRepository, _deps.PlayerRepository); } }
        public DeleteCheckpoint DeleteCheckpoint { get { return new DeleteCheckpoint(_deps.BunchRepository, _deps.CashgameRepository, _deps.CheckpointRepository); } }
        public Buyin Buyin { get { return new Buyin(_deps.BunchRepository, _deps.PlayerRepository, _deps.CashgameRepository, _deps.CheckpointRepository); } }
        public Report Report { get { return new Report(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.CheckpointRepository); } }
        public Cashout Cashout { get { return new Cashout(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.CheckpointRepository); } }
        public EndCashgame EndCashgame { get { return new EndCashgame(_deps.BunchRepository, _deps.CashgameRepository); } }
        
        // Player
        public PlayerList PlayerList { get { return new PlayerList(_deps.BunchRepository, _deps.UserRepository, _deps.PlayerRepository); } }
        public PlayerDetails PlayerDetails { get { return new PlayerDetails(_deps.BunchRepository, _deps.PlayerRepository, _deps.CashgameRepository, _deps.UserRepository); } }
        public PlayerFacts PlayerFacts { get { return new PlayerFacts(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository); } }
        public PlayerBadges PlayerBadges { get { return new PlayerBadges(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.UserRepository); } }
        public InvitePlayer InvitePlayer { get { return new InvitePlayer(_deps.BunchRepository, _deps.PlayerRepository, _deps.MessageSender); } }
        public AddPlayer AddPlayer { get { return new AddPlayer(_deps.BunchRepository, _deps.PlayerRepository); } }
        public DeletePlayer DeletePlayer { get { return new DeletePlayer(_deps.PlayerRepository, _deps.CashgameRepository); } }
    }
}