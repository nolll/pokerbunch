using System;
using Application.Factories;
using Application.Services;
using Application.UseCases.Actions;
using Application.UseCases.AddBunchForm;
using Application.UseCases.AddCashgame;
using Application.UseCases.AddCashgameForm;
using Application.UseCases.AddPlayer;
using Application.UseCases.AddUser;
using Application.UseCases.AppContext;
using Application.UseCases.BaseContext;
using Application.UseCases.BunchContext;
using Application.UseCases.BunchDetails;
using Application.UseCases.BunchList;
using Application.UseCases.Buyin;
using Application.UseCases.BuyinForm;
using Application.UseCases.CashgameChartContainer;
using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameDetails;
using Application.UseCases.CashgameFacts;
using Application.UseCases.CashgameList;
using Application.UseCases.CashgameTopList;
using Application.UseCases.DeletePlayer;
using Application.UseCases.EditBunchForm;
using Application.UseCases.EditCheckpointForm;
using Application.UseCases.EditUserForm;
using Application.UseCases.ForgotPassword;
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

        public static DependencyContainer Instance
        {
            get { return _instance ?? (_instance = new DependencyContainer()); }
        }

        private DependencyContainer()
        {
        }

        // Contexts
        public Func<BaseContextResult> BaseContext { get { return () => BaseContextInteractor.Execute(WebContext); } }
        public Func<AppContextResult> AppContext { get { return () => AppContextInteractor.Execute(BaseContext, Auth); } }
        public Func<BunchContextRequest, BunchContextResult> BunchContext { get { return request => BunchContextInteractor.Execute(AppContext, BunchRepository, Auth, request); } }
        public Func<CashgameContextRequest, CashgameContextResult> CashgameContext { get { return request => CashgameContextInteractor.Execute(BunchContext, CashgameRepository, request); } }

        // Auth and Home
        public Func<HomeResult> Home { get { return () => HomeInteractor.Execute(Auth); } }
        public Func<LoginFormRequest, LoginFormResult> LoginForm { get { return LoginFormInteractor.Execute; } }
        public Func<LoginRequest, LoginResult> Login { get { return request => LoginInteractor.Execute(UserRepository, Auth, BunchRepository, PlayerRepository, request); } }
        public Func<LogoutResult> Logout { get { return () => LogoutInteractor.Execute(Auth); } }

        // Admin
        public Func<TestEmailResult> TestEmail { get { return () => TestEmailInteractor.Execute(MessageSender); } }

        // User
        public Func<UserListResult> UserList { get { return () => UserListInteractor.Execute(UserRepository); } }
        public Func<UserDetailsRequest, UserDetailsResult> UserDetails { get { return request => UserDetailsInteractor.Execute(Auth, UserRepository, request); } }
        public Func<AddUserRequest, AddUserResult> AddUser { get { return request => AddUserInteractor.Execute(UserRepository, RandomService, MessageSender, request); } }
        public Func<EditUserFormRequest, EditUserFormResult> EditUserForm { get { return request => EditUserFormInteractor.Execute(UserRepository, request); } }
        public Func<ForgotPasswordRequest, ForgotPasswordResult> ForgotPassword { get { return request => ForgotPasswordInteractor.Execute(UserRepository, MessageSender, RandomService, request); } }

        // Bunch
        public Func<BunchListResult> BunchList { get { return () => BunchListInteractor.Execute(BunchRepository); } }
        public Func<BunchDetailsRequest, BunchDetailsResult> BunchDetails { get { return request => BunchDetailsInteractor.Execute(BunchRepository, Auth, request); } }
        public Func<AddBunchFormResult> AddBunchForm { get { return AddBunchFormInteractor.Execute; } }
        public Func<EditBunchFormRequest, EditBunchFormResult> EditBunchForm { get { return request => EditBunchFormInteractor.Execute(BunchRepository, request); } }
        public Func<JoinBunchFormRequest, JoinBunchFormResult> JoinBunchForm { get { return request => JoinBunchFormInteractor.Execute(BunchRepository, request); } }
        public Func<JoinBunchConfirmationRequest, JoinBunchConfirmationResult> JoinBunchConfirmation { get { return request => JoinBunchConfirmationInteractor.Execute(BunchRepository, request); } }

        // Cashgame
        public Func<TopListRequest, TopListResult> TopList { get { return request => TopListInteractor.Execute(BunchRepository, CashgameService, request); } }
        public Func<CashgameDetailsRequest, CashgameDetailsResult> CashgameDetails { get { return request => CashgameDetailsInteractor.Execute(BunchRepository, CashgameRepository, Auth, PlayerRepository, request); } }
        public Func<CashgameFactsRequest, CashgameFactsResult> CashgameFacts { get { return request => CashgameFactsInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, request); } }
        public Func<CashgameListRequest, CashgameListResult> CashgameList { get { return request => CashgameListInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<AddCashgameFormRequest, AddCashgameFormResult> AddCashgameForm { get { return request => AddCashgameFormInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<AddCashgameRequest, AddCashgameResult> AddCashgame { get { return request => AddCashgameInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<ActionsRequest, ActionsResult> Actions { get { return request => ActionsInteractor.Execute(BunchRepository, CashgameRepository, PlayerRepository, Auth, request); } }
        public Func<BuyinFormRequest, BuyinFormResult> BuyinForm { get { return request => BuyinFormInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<BuyinRequest, BuyinResult> Buyin { get { return request => BuyinInteractor.Execute(BunchRepository, PlayerRepository, CashgameRepository, CheckpointRepository, TimeProvider, request); } }
        public Func<EditCheckpointFormRequest, EditCheckpointFormResult> EditCheckpointForm { get { return request => EditCheckpointFormInteractor.Execute(BunchRepository, CheckpointRepository, request); } }
        public Func<CashgameChartContainerRequest, CashgameChartContainerResult> CashgameChartContainer { get { return CashgameChartContainerInteractor.Execute; } }

        // Player
        public Func<PlayerListRequest, PlayerListResult> PlayerList { get { return request => PlayerListInteractor.Execute(BunchRepository, PlayerRepository, Auth, request); } }
        public Func<PlayerDetailsRequest, PlayerDetailsResult> PlayerDetails { get { return request => PlayerDetailsInteractor.Execute(Auth, BunchRepository, PlayerRepository, CashgameRepository, UserRepository, request); } }
        public Func<PlayerFactsRequest, PlayerFactsResult> PlayerFacts { get { return request => PlayerFactsInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<PlayerBadgesRequest, PlayerBadgesResult> PlayerBadges { get { return request => PlayerBadgesInteractor.Execute(BunchRepository, CashgameRepository, request); } }
        public Func<InvitePlayerRequest, InvitePlayerResult> InvitePlayer { get { return request => InvitePlayerInteractor.Execute(BunchRepository, PlayerRepository, MessageSender, request); } }
        public Func<AddPlayerRequest, AddPlayerResult> AddPlayer { get { return request => AddPlayerInteractor.Execute(BunchRepository, PlayerRepository, request); } }
        public Func<DeletePlayerRequest, DeletePlayerResult> DeletePlayer { get { return request => DeletePlayerInteractor.Execute(BunchRepository, PlayerRepository, CashgameRepository, request); } }

        private ITimeProvider TimeProvider { get { return new TimeProvider(); } }
        private IRandomService RandomService { get { return new RandomService(); } }
        private IWebContext WebContext { get { return new WebContext(); } }
        private IStorageProvider StorageProvider { get { return new SqlServerStorageProvider(); } }
        private ICacheProvider CacheProvider { get { return new CacheProvider(); } }
        private ICacheContainer CacheContainer { get { return new CacheContainer(CacheProvider); } }
        private ICacheBuster CacheBuster { get { return new CacheBuster(CacheContainer); } }
        private IBunchStorage BunchStorage { get { return new SqlServerBunchStorage(StorageProvider); } }
        private IBunchRepository BunchRepository { get { return new BunchRepository(BunchStorage, CacheContainer, CacheBuster); } }
        private IUserStorage UserStorage { get { return new SqlServerUserStorage(StorageProvider); } }
        private IUserRepository UserRepository { get { return new UserRepository(UserStorage, CacheContainer, CacheBuster); } }
        private IPlayerStorage PlayerStorage { get { return new SqlServerPlayerStorage(StorageProvider); } }
        private IPlayerDataMapper PlayerDataMapper { get { return new PlayerDataMapper(UserRepository); } }
        private IPlayerRepository PlayerRepository { get { return new PlayerRepository(PlayerStorage, PlayerDataMapper, CacheContainer, CacheBuster); } }
        private IRawCashgameFactory RawCashgameFactory { get { return new RawCashgameFactory(TimeProvider); } }
        private ICheckpointStorage CheckpointStorage { get { return new SqlServerCheckpointStorage(StorageProvider, TimeProvider); } }
        private ICashgameStorage CashgameStorage { get { return new SqlServerCashgameStorage(StorageProvider, RawCashgameFactory); } }
        private ICashgameResultFactory CashgameResultFactory { get { return new CashgameResultFactory(TimeProvider); } }
        private ICashgameDataMapper CashgameDataMapper { get { return new CashgameDataMapper(CashgameResultFactory); } }
        private ICashgameRepository CashgameRepository { get { return new CashgameRepository(CashgameStorage, RawCashgameFactory, CacheContainer, CheckpointStorage, CacheBuster, CashgameDataMapper); } }
        private ICashgameTotalResultFactory CashgameTotalResultFactory { get { return new CashgameTotalResultFactory(); } }
        private ICashgameSuiteFactory CashgameSuiteFactory { get { return new CashgameSuiteFactory(CashgameTotalResultFactory); } }
        private ICashgameService CashgameService { get { return new CashgameService(PlayerRepository, CashgameRepository, CashgameSuiteFactory, BunchRepository); } }
        private ICheckpointRepository CheckpointRepository { get { return new CheckpointRepository(CheckpointStorage, CacheBuster); } }
        private IAuth Auth { get { return new Auth(TimeProvider, UserRepository); } }
        private IMessageSender MessageSender { get { return new MessageSender(); } }
    }
}