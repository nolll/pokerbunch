using Core.UseCases;
using Core.UseCases.CashgameCurrentRankings;
using Core.UseCases.EditCheckpoint;
using Core.UseCases.EditCheckpointForm;
using Core.UseCases.EditUser;
using Core.UseCases.EditUserForm;
using Core.UseCases.JoinBunchConfirmation;
using Core.UseCases.JoinBunchForm;
using Core.UseCases.Matrix;
using Core.UseCases.PlayerBadges;
using Core.UseCases.PlayerFacts;
using Core.UseCases.PlayerList;
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
        public Login Login { get { return new Login(_deps.UserRepository); } }

        // Admin
        public TestEmail TestEmail { get { return new TestEmail(_deps.MessageSender); } }
        public ClearCache ClearCache { get { return new ClearCache(_deps.CacheContainer); } }

        // User
        public UserListInteractor UserList { get { return new UserListInteractor(_deps.UserRepository); } }
        public UserDetails UserDetails { get { return new UserDetails(_deps.UserRepository); } }
        public AddUser AddUser { get { return new AddUser(_deps.UserRepository, _deps.RandomService, _deps.MessageSender); } }
        public EditUserFormInteractor EditUserForm { get { return new EditUserFormInteractor(_deps.UserRepository); } }
        public EditUserInteractor EditUser { get { return new EditUserInteractor(_deps.UserRepository); } }
        public ForgotPassword ForgotPassword { get { return new ForgotPassword(_deps.UserRepository, _deps.MessageSender, _deps.RandomService); } }
        public ChangePassword ChangePassword { get { return new ChangePassword(_deps.UserRepository, _deps.RandomService); } } 

        // Bunch
        public BunchList BunchList { get { return new BunchList(_deps.BunchRepository, _deps.UserRepository); } }
        public BunchDetails BunchDetails { get { return new BunchDetails(_deps.BunchRepository, _deps.UserRepository, _deps.PlayerRepository); } }
        public AddBunchForm AddBunchForm { get { return new AddBunchForm(); } }
        public AddBunch AddBunch { get { return new AddBunch(_deps.UserRepository, _deps.BunchRepository, _deps.PlayerRepository); } }
        public EditBunchForm EditBunchForm { get { return new EditBunchForm(_deps.BunchRepository); } }
        public EditBunch EditBunch { get { return new EditBunch(_deps.BunchRepository); } }
        public JoinBunchFormInteractor JoinBunchForm { get { return new JoinBunchFormInteractor(_deps.BunchRepository); } }
        public JoinBunch JoinBunch { get { return new JoinBunch(_deps.BunchRepository, _deps.PlayerRepository, _deps.UserRepository); } }
        public JoinBunchConfirmationInteractor JoinBunchConfirmation { get { return new JoinBunchConfirmationInteractor(_deps.BunchRepository); } }

        // Events
        public EventList EventList { get { return new EventList(_deps.BunchRepository, _deps.EventRepository); } }
        public EventDetails EventDetails { get { return new EventDetails(_deps.EventRepository); } } 

        // Cashgame
        public CashgameStatus CashgameStatus { get { return new CashgameStatus(_deps.BunchRepository, _deps.CashgameRepository); } }
        public TopList TopList { get { return new TopList(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
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
        public EditCashgameForm EditCashgameForm { get { return new EditCashgameForm(_deps.BunchRepository, _deps.CashgameRepository); } }
        public EditCashgame EditCashgame { get { return new EditCashgame(_deps.BunchRepository, _deps.CashgameRepository); } }
        public DeleteCashgame DeleteCashgame { get { return new DeleteCashgame(_deps.CashgameRepository); } }
        public DeleteCheckpoint DeleteCheckpoint { get { return new DeleteCheckpoint(_deps.BunchRepository, _deps.CashgameRepository, _deps.CheckpointRepository); } }
        public Buyin Buyin { get { return new Buyin(_deps.BunchRepository, _deps.PlayerRepository, _deps.CashgameRepository, _deps.CheckpointRepository); } }
        public Report Report { get { return new Report(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.CheckpointRepository); } }
        public Cashout Cashout { get { return new Cashout(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository, _deps.CheckpointRepository); } }
        public EndCashgame EndCashgame { get { return new EndCashgame(_deps.BunchRepository, _deps.CashgameRepository); } }
        
        // Player
        public PlayerListInteractor PlayerList { get { return new PlayerListInteractor(_deps.BunchRepository, _deps.UserRepository, _deps.PlayerRepository); } }
        public PlayerDetails PlayerDetails { get { return new PlayerDetails(_deps.BunchRepository, _deps.PlayerRepository, _deps.CashgameRepository, _deps.UserRepository); } }
        public PlayerFactsInteractor PlayerFacts { get { return new PlayerFactsInteractor(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
        public PlayerBadgesInteractor PlayerBadges { get { return new PlayerBadgesInteractor(_deps.BunchRepository, _deps.CashgameRepository, _deps.PlayerRepository); } }
        public InvitePlayer InvitePlayer { get { return new InvitePlayer(_deps.BunchRepository, _deps.PlayerRepository, _deps.MessageSender); } }
        public AddPlayer AddPlayer { get { return new AddPlayer(_deps.BunchRepository, _deps.PlayerRepository); } }
        public DeletePlayer DeletePlayer { get { return new DeletePlayer(_deps.PlayerRepository, _deps.CashgameRepository); } }
    }
}