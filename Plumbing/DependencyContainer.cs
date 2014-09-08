using System;
using Application.Factories;
using Application.Services;
using Application.UseCases.Actions;
using Application.UseCases.AddBunchForm;
using Application.UseCases.AddCashgame;
using Application.UseCases.AddCashgameForm;
using Application.UseCases.AddPlayer;
using Application.UseCases.AppContext;
using Application.UseCases.BaseContext;
using Application.UseCases.BunchContext;
using Application.UseCases.BunchDetails;
using Application.UseCases.BunchList;
using Application.UseCases.Buyin;
using Application.UseCases.BuyinForm;
using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameDetails;
using Application.UseCases.CashgameFacts;
using Application.UseCases.CashgameTopList;
using Application.UseCases.DeletePlayer;
using Application.UseCases.EditBunchForm;
using Application.UseCases.EditUserForm;
using Application.UseCases.Home;
using Application.UseCases.InvitePlayer;
using Application.UseCases.JoinBunchConfirmation;
using Application.UseCases.JoinBunchForm;
using Application.UseCases.Login;
using Application.UseCases.LoginForm;
using Application.UseCases.Logout;
using Application.UseCases.PlayerBadges;
using Application.UseCases.PlayerDetails;
using Application.UseCases.PlayerFacts;
using Application.UseCases.PlayerList;
using Application.UseCases.TestEmail;
using Application.UseCases.UserDetails;
using Application.UseCases.UserList;
using Core.Factories;
using Core.Factories.Interfaces;
using Core.Repositories;
using Core.Services;
using Core.Services.Interfaces;
using Infrastructure.Data.Cache;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Mappers;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.SqlServer;
using Infrastructure.System;
using Infrastructure.Web;

namespace Plumbing
{
    public class DependencyContainer
    {
        private static DependencyContainer _instance;

        public Func<BaseContextResult> BaseContext { get; private set; }
        public Func<AppContextResult> AppContext { get; private set; }
        public Func<BunchContextRequest, BunchContextResult> BunchContext { get; private set; }
        public Func<CashgameContextRequest, CashgameContextResult> CashgameContext { get; private set; }

        public Func<HomeResult> Home { get; private set; }
        public Func<LoginFormRequest, LoginFormResult> LoginForm { get; private set; }
        public Func<LoginRequest, LoginResult> Login { get; private set; }
        public Func<LogoutResult> Logout { get; private set; }

        public Func<TestEmailResult> TestEmail { get; private set; }

        public Func<UserListResult> UserList { get; private set; }
        public Func<UserDetailsRequest, UserDetailsResult> UserDetails { get; private set; }
        public Func<EditUserFormRequest, EditUserFormResult> EditUserForm { get; private set; }

        public Func<BunchListResult> BunchList { get; private set; }
        public Func<BunchDetailsRequest, BunchDetailsResult> BunchDetails { get; private set; }
        public Func<AddBunchFormResult> AddBunchForm { get; private set; }
        public Func<EditBunchFormRequest, EditBunchFormResult> EditBunchForm { get; private set; }
        public Func<JoinBunchFormRequest, JoinBunchFormResult> JoinBunchForm { get; private set; }
        public Func<JoinBunchConfirmationRequest, JoinBunchConfirmationResult> JoinBunchConfirmation { get; private set; }

        public Func<TopListRequest, TopListResult> TopList { get; private set; }
        public Func<CashgameDetailsRequest, CashgameDetailsResult> CashgameDetails { get; private set; }
        public Func<CashgameFactsRequest, CashgameFactsResult> CashgameFacts { get; private set; }
        public Func<AddCashgameFormRequest, AddCashgameFormResult> AddCashgameForm { get; private set; }
        public Func<AddCashgameRequest, AddCashgameResult> AddCashgame { get; private set; }
        public Func<ActionsRequest, ActionsResult> Actions { get; private set; }
        public Func<BuyinFormRequest, BuyinFormResult> BuyinForm { get; private set; }
        public Func<BuyinRequest, BuyinResult> Buyin { get; private set; }

        public Func<PlayerListRequest, PlayerListResult> PlayerList { get; private set; }
        public Func<PlayerDetailsRequest, PlayerDetailsResult> PlayerDetails { get; private set; }
        public Func<PlayerFactsRequest, PlayerFactsResult> PlayerFacts { get; private set; }
        public Func<PlayerBadgesRequest, PlayerBadgesResult> PlayerBadges { get; private set; }
        public Func<InvitePlayerRequest, InvitePlayerResult> InvitePlayer { get; private set; }
        public Func<AddPlayerRequest, AddPlayerResult> AddPlayer { get; private set; }
        public Func<DeletePlayerRequest, DeletePlayerResult> DeletePlayer { get; private set; }

        private DependencyContainer()
        {
            BaseContext = () => BaseContextInteractor.Execute(WebContext);
            AppContext = () => AppContextInteractor.Execute(BaseContext, Auth);
            BunchContext = request => BunchContextInteractor.Execute(AppContext, BunchRepository, Auth, request);
            CashgameContext = request => CashgameContextInteractor.Execute(BunchContext, CashgameRepository, request);

            Home = () => HomeInteractor.Execute(Auth);
            LoginForm = LoginFormInteractor.Execute;
            Login = request => LoginInteractor.Execute(UserRepository, Auth, BunchRepository, PlayerRepository, request);
            Logout = () => LogoutInteractor.Execute(Auth); 

            TestEmail = () => TestEmailInteractor.Execute(MessageSender);

            UserList = () => UserListInteractor.Execute(UserRepository);
            UserDetails = request => UserDetailsInteractor.Execute(Auth, UserRepository, request);
            EditUserForm = request => EditUserFormInteractor.Execute(UserRepository, request);

            BunchList = () => BunchListInteractor.Execute(BunchRepository);
            BunchDetails = request => BunchDetailsInteractor.Execute(BunchRepository, Auth, request);
            AddBunchForm = AddBunchFormInteractor.Execute;;
            EditBunchForm = request => EditBunchFormInteractor.Execute(BunchRepository, request);
            JoinBunchForm = request => JoinBunchFormInteractor.Execute(BunchRepository, request);
            JoinBunchConfirmation = request => JoinBunchConfirmationInteractor.Execute(BunchRepository, request);

            TopList = request => TopListInteractor.Execute(BunchRepository, CashgameService, request);
            CashgameDetails = request => CashgameDetailsInteractor.Execute(BunchRepository, CashgameRepository, Auth, PlayerRepository, request);
            CashgameFacts = request => CashgameFactsInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, request);
            AddCashgameForm = request => AddCashgameFormInteractor.Execute(BunchRepository, CashgameRepository, request);
            AddCashgame = request => AddCashgameInteractor.Execute(BunchRepository, CashgameRepository, request);
            Actions = request => ActionsInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, Auth, request);
            BuyinForm = request => BuyinFormInteractor.Execute(BunchRepository, CashgameRepository, request);
            Buyin = request => BuyinInteractor.Execute(BunchRepository, PlayerRepository, CashgameRepository, CheckpointRepository, TimeProvider, request);

            PlayerList = request => PlayerListInteractor.Execute(BunchRepository, PlayerRepository, Auth, request);
            PlayerDetails = request => PlayerDetailsInteractor.Execute(Auth, BunchRepository, PlayerRepository, CashgameRepository, UserRepository, request);
            PlayerFacts = request => PlayerFactsInteractor.Execute(BunchRepository, CashgameRepository, request);
            PlayerBadges = request => PlayerBadgesInteractor.Execute(BunchRepository, CashgameRepository, request);
            InvitePlayer = request => InvitePlayerInteractor.Execute(BunchRepository, PlayerRepository, MessageSender, request);
            AddPlayer = request => AddPlayerInteractor.Execute(BunchRepository, PlayerRepository, request);
            DeletePlayer = request => DeletePlayerInteractor.Execute(BunchRepository, PlayerRepository, CashgameRepository, request);
        }

        private ITimeProvider TimeProvider
        {
            get { return new TimeProvider(); }
        }

        private IWebContext WebContext
        {
            get { return new WebContext(); }
        }

        private IStorageProvider StorageProvider
        {
            get { return new SqlServerStorageProvider(); }
        }

        private ICacheProvider CacheProvider
        {
            get { return new CacheProvider(); }
        }

        private ICacheContainer CacheContainer
        {
            get { return new CacheContainer(CacheProvider); }
        }

        private ICacheBuster CacheBuster
        {
            get { return new CacheBuster(CacheContainer); }
        }

        private IBunchStorage BunchStorage
        {
            get { return new SqlServerBunchStorage(StorageProvider); }
        }

        private IBunchRepository BunchRepository
        {
            get { return new BunchRepository(BunchStorage, CacheContainer, CacheBuster); }
        }

        private IUserStorage UserStorage
        {
            get { return new SqlServerUserStorage(StorageProvider); }
        }

        private IUserRepository UserRepository
        {
            get { return new UserRepository(UserStorage, CacheContainer, CacheBuster); }
        }

        private IPlayerStorage PlayerStorage
        {
            get { return new SqlServerPlayerStorage(StorageProvider); }
        }

        private IPlayerDataMapper PlayerDataMapper
        {
            get { return new PlayerDataMapper(UserRepository); }
        }

        private IPlayerRepository PlayerRepository
        {
            get { return new PlayerRepository(PlayerStorage, PlayerDataMapper, CacheContainer, CacheBuster); }
        }

        private IRawCashgameFactory RawCashgameFactory
        {
            get { return new RawCashgameFactory(TimeProvider); }
        }

        private ICheckpointStorage CheckpointStorage
        {
            get { return new SqlServerCheckpointStorage(StorageProvider, TimeProvider); }
        }

        private ICashgameStorage CashgameStorage
        {
            get { return new SqlServerCashgameStorage(StorageProvider, RawCashgameFactory); }
        }

        private ICashgameResultFactory CashgameResultFactory
        {
            get { return new CashgameResultFactory(TimeProvider); }
        }

        private ICashgameDataMapper CashgameDataMapper
        {
            get { return new CashgameDataMapper(CashgameResultFactory); }
        }

        private ICashgameRepository CashgameRepository
        {
            get { return new CashgameRepository(CashgameStorage, RawCashgameFactory, CacheContainer, CheckpointStorage, CacheBuster, CashgameDataMapper); }
        }

        private ICashgameTotalResultFactory CashgameTotalResultFactory
        {
            get { return new CashgameTotalResultFactory(); }
        }

        private ICashgameSuiteFactory CashgameSuiteFactory
        {
            get { return new CashgameSuiteFactory(CashgameTotalResultFactory); }
        }

        private ICashgameService CashgameService
        {
            get { return new CashgameService(PlayerRepository, CashgameRepository, CashgameSuiteFactory, BunchRepository); }
        }

        private ICheckpointRepository CheckpointRepository
        {
            get { return new CheckpointRepository(CheckpointStorage, CacheBuster); }
        }

        private IAuth Auth
        {
            get { return new Auth(TimeProvider, UserRepository); }
        }

        private IMessageSender MessageSender
        {
            get { return new MessageSender(); }
        }

        public static DependencyContainer Instance
        {
            get { return _instance ?? (_instance = new DependencyContainer()); }
        }
    }
}