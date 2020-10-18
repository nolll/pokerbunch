using Core.Services;
using Core.Settings;
using Core.UseCases;
using Infrastructure.Api.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PokerBunch.Client.Clients;
using PokerBunch.Client.Connection;
using PokerBunch.Common.Urls;
using Web.Services;
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
            _services.AddHttpContextAccessor();

            _services.AddScoped<ITokenReader, TokenReader>();
            _services.AddScoped(o => _settings);
            _services.AddScoped<IUrlFormatter>(o => new UrlFormatter(_settings.Urls.SiteUri, _settings.Urls.ApiUri));
            _services.AddScoped(o => new ApiConnection(_settings.ApiKey, _settings.DetailedErrorsForApi, o.GetService<IUrlFormatter>(), o.GetService<ITokenReader>()));
            _services.AddScoped(o => new PokerBunchClient(o.GetService<ApiConnection>()));
            
            _services.AddScoped<ILocationService, LocationService>();
            _services.AddScoped<IBunchService, BunchService>();
            _services.AddScoped<ICashgameService, CashgameService>();
            _services.AddScoped<IEventService, EventService>();
            _services.AddScoped<IPlayerService, PlayerService>();
            _services.AddScoped<IUserService, UserService>();
            _services.AddScoped<IAuthService, AuthService>();

            // Contexts
            _services.AddScoped<CoreContext, CoreContext>();
            _services.AddScoped<BunchContext, BunchContext>();

            // Auth and Home
            _services.AddScoped<Login>();

            // User
            _services.AddScoped<AddUser>();

            // Bunch
            _services.AddScoped<AddBunchForm>();
            _services.AddScoped<AddBunch>();
            _services.AddScoped<EditBunchForm>();
            _services.AddScoped<EditBunch>();
            _services.AddScoped<JoinBunchForm>();
            _services.AddScoped<JoinBunch>();
            _services.AddScoped<JoinBunchConfirmation>();

            // Events
            _services.AddScoped<EventDetails>();
            _services.AddScoped<AddEvent>();

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