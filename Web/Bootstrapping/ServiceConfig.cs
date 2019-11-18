﻿using Core.Services;
using Core.Settings;
using Core.UseCases;
using Infrastructure.Api.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Plumbing;
using PokerBunch.Client.Clients;
using PokerBunch.Client.Connection;
using PokerBunch.Common.Urls;
using CashgameService = Infrastructure.Api.Services.CashgameService;

namespace Web.Bootstrapping
{
    public class ServiceConfig
    {
        private readonly AppSettings _settings;
        private readonly IServiceCollection _services;

        public ServiceConfig(AppSettings settings, IServiceCollection services)
        {
            _settings = settings;
            _services = services;
        }

        public void Configure()
        {
            AddCompression();
            AddLogging();
            AddDependencies();
            AddAuthentication();
            AddMvc();
            AddCors();
        }

        private void AddCompression()
        {
            _services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });
        }

        private void AddLogging()
        {
            _services.AddLogging(logging =>
            {
                if (_settings.Logging.Loggers.Debug)
                    logging.AddDebug();

                if (_settings.Logging.Loggers.Console)
                    logging.AddConsole();

                logging.SetMinimumLevel(_settings.Logging.LogLevel);
            });
        }

        private void AddDependencies()
        {
            var urlFormatter = new UrlFormatter(_settings.Urls.SiteUri, _settings.Urls.ApiUri);
            var apiConnection = new ApiConnection(_settings.ApiKey, "", _settings.DetailedErrorsForApi, urlFormatter);
            var apiClient = new PokerBunchClient(apiConnection);

            _services.AddScoped(o => _settings);
            _services.AddScoped<IUrlFormatter>(o => urlFormatter);
            _services.AddScoped(o => apiConnection);
            _services.AddScoped(o => apiClient);
            _services.AddScoped(o => new ServiceFactory(apiClient, _settings.UseFakeData));

            _services.AddScoped<ILocationService, LocationService>();
            _services.AddScoped<IBunchService, BunchService>();
            _services.AddScoped<IAppService, AppService>();
            _services.AddScoped<ICashgameService, CashgameService>();
            _services.AddScoped<IEventService, EventService>();
            _services.AddScoped<IPlayerService, PlayerService>();
            _services.AddScoped<IUserService, UserService>();
            _services.AddScoped<IAuthService, AuthService>();
            _services.AddScoped<IAdminService, AdminService>();

            // Contexts
            _services.AddScoped<CoreContext, CoreContext>();
            _services.AddScoped<BunchContext, BunchContext>();

            // Auth and Home
            _services.AddScoped<Login>();

            // Admin
            _services.AddScoped<TestEmail>();
            _services.AddScoped<ClearCache>();

            // User
            _services.AddScoped<UserDetails>();
            _services.AddScoped<AddUser>();
            _services.AddScoped<EditUserForm>();
            _services.AddScoped<EditUser>();
            _services.AddScoped<ForgotPassword>();
            _services.AddScoped<ChangePassword>();

            // Bunch
            _services.AddScoped<BunchList>();
            _services.AddScoped<AddBunchForm>();
            _services.AddScoped<AddBunch>();
            _services.AddScoped<EditBunchForm>();
            _services.AddScoped<EditBunch>();
            _services.AddScoped<JoinBunchForm>();
            _services.AddScoped<JoinBunch>();
            _services.AddScoped<JoinBunchConfirmation>();

            // Events
            _services.AddScoped<EventList>();
            _services.AddScoped<EventDetails>();
            _services.AddScoped<AddEvent>();

            // Locations
            _services.AddScoped<LocationList>();
            _services.AddScoped<LocationDetails>();
            _services.AddScoped<AddLocation>();

            // Cashgame
            _services.AddScoped<AddCashgameForm>();
            _services.AddScoped<AddCashgame>();
            _services.AddScoped<EventMatrix>();
            _services.AddScoped<EditCashgameForm>();
            _services.AddScoped<EditCashgame>();
            _services.AddScoped<DeleteCashgame>();

            // Player
            _services.AddScoped<PlayerDetails>();
            _services.AddScoped<PlayerFacts>();
            _services.AddScoped<PlayerBadges>();
            _services.AddScoped<InvitePlayer>();
            _services.AddScoped<InvitePlayerForm>();
            _services.AddScoped<InvitePlayerConfirmation>();
            _services.AddScoped<AddPlayer>();
            _services.AddScoped<DeletePlayer>();

            // Apps
            _services.AddScoped<AppDetails>();
            _services.AddScoped<AppListUser>();
            _services.AddScoped<AppListAll>();
            _services.AddScoped<AddApp>();
            _services.AddScoped<DeleteApp>();
        }

        private void AddAuthentication()
        {
            _services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
        }

        private void AddMvc()
        {
            _services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
        }

        private void AddCors()
        {
            _services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }
    }
}