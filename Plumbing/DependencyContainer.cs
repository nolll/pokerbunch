using System;
using Application.Factories;
using Application.UseCases.Actions;
using Application.UseCases.AddBunchForm;
using Application.UseCases.AddCashgame;
using Application.UseCases.AddCashgameForm;
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
using Core.Services;
using Infrastructure.Data.Cache;
using Infrastructure.Data.Factories;
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

        private DependencyContainer()
        {
            var timeProvider = new TimeProvider();
            var webContext = new WebContext();

            var storageProvider = new SqlServerStorageProvider();
            var cacheProvider = new CacheProvider();
            var cacheContainer = new CacheContainer(cacheProvider);
            var cacheBuster = new CacheBuster(cacheContainer);
            
            var bunchStorage = new SqlServerBunchStorage(storageProvider);
            var bunchRepository = new BunchRepository(bunchStorage, cacheContainer, cacheBuster);

            var userStorage = new SqlServerUserStorage(storageProvider);
            var userRepository = new UserRepository(userStorage, cacheContainer, cacheBuster);

            var playerStorage = new SqlServerPlayerStorage(storageProvider);
            var playerDataMapper = new PlayerDataMapper(userRepository);
            var playerRepository = new PlayerRepository(playerStorage, playerDataMapper, cacheContainer, cacheBuster);

            var rawCashgameFactory = new RawCashgameFactory(timeProvider);
            var checkpointStorage = new SqlServerCheckpointStorage(storageProvider, timeProvider);
            var cashgameStorage = new SqlServerCashgameStorage(storageProvider, rawCashgameFactory);
            var cashgameResultFactory = new CashgameResultFactory(timeProvider);
            var cashgameDataMapper = new CashgameDataMapper(cashgameResultFactory);
            var cashgameRepository = new CashgameRepository(cashgameStorage, rawCashgameFactory, cacheContainer, checkpointStorage, cacheBuster, cashgameDataMapper);
            var cashgameTotalResultFactory = new CashgameTotalResultFactory();
            var cashgameSuiteFactory = new CashgameSuiteFactory(cashgameTotalResultFactory);
            var cashgameService = new CashgameService(playerRepository, cashgameRepository, cashgameSuiteFactory, bunchRepository);
            var checkpointRepository = new CheckpointRepository(checkpointStorage, cacheBuster);

            var auth = new Auth(timeProvider, userRepository);
            var messageSender = new MessageSender();

            BaseContext = () => BaseContextInteractor.Execute(webContext);
            AppContext = () => AppContextInteractor.Execute(BaseContext, auth);
            BunchContext = request => BunchContextInteractor.Execute(AppContext, bunchRepository, auth, request);
            CashgameContext = request => CashgameContextInteractor.Execute(BunchContext, cashgameRepository, request);

            Home = () => HomeInteractor.Execute(auth);
            LoginForm = LoginFormInteractor.Execute;
            Login = request => LoginInteractor.Execute(userRepository, auth, bunchRepository, playerRepository, request);
            Logout = () => LogoutInteractor.Execute(auth); 

            TestEmail = () => TestEmailInteractor.Execute(messageSender);

            UserList = () => UserListInteractor.Execute(userRepository);
            UserDetails = request => UserDetailsInteractor.Execute(auth, userRepository, request);
            EditUserForm = request => EditUserFormInteractor.Execute(userRepository, request);

            BunchList = () => BunchListInteractor.Execute(bunchRepository);
            BunchDetails = request => BunchDetailsInteractor.Execute(bunchRepository, auth, request);
            AddBunchForm = AddBunchFormInteractor.Execute;;
            EditBunchForm = request => EditBunchFormInteractor.Execute(bunchRepository, request);
            JoinBunchForm = request => JoinBunchFormInteractor.Execute(bunchRepository, request);
            JoinBunchConfirmation = request => JoinBunchConfirmationInteractor.Execute(bunchRepository, request);

            TopList = request => TopListInteractor.Execute(bunchRepository, cashgameService, request);
            CashgameDetails = request => CashgameDetailsInteractor.Execute(bunchRepository, cashgameRepository, auth, playerRepository, request);
            CashgameFacts = request => CashgameFactsInteractor.Execute(bunchRepository, cashgameRepository, playerRepository, request);
            AddCashgameForm = request => AddCashgameFormInteractor.Execute(bunchRepository, cashgameRepository, request);
            AddCashgame = request => AddCashgameInteractor.Execute(bunchRepository, cashgameRepository, request);
            Actions = request => ActionsInteractor.Execute(bunchRepository, cashgameRepository, playerRepository, auth, request);
            BuyinForm = request => BuyinFormInteractor.Execute(bunchRepository, cashgameRepository, request);
            Buyin = request => BuyinInteractor.Execute(bunchRepository, playerRepository, cashgameRepository, checkpointRepository, timeProvider, request);

            PlayerList = request => PlayerListInteractor.Execute(bunchRepository, playerRepository, auth, request);
            PlayerDetails = request => PlayerDetailsInteractor.Execute(auth, bunchRepository, playerRepository, cashgameRepository, userRepository, request);
            PlayerFacts = request => PlayerFactsInteractor.Execute(bunchRepository, cashgameRepository, request);
            PlayerBadges = request => PlayerBadgesInteractor.Execute(bunchRepository, cashgameRepository, request);
            InvitePlayer = request => InvitePlayerInteractor.Execute(bunchRepository, playerRepository, messageSender, request);
        }

        public static DependencyContainer Instance
        {
            get { return _instance ?? (_instance = new DependencyContainer()); }
        }
    }
}