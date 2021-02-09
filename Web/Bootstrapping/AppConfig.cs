using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Web.Middleware;

namespace Web.Bootstrapping
{
    public class AppConfig
    {
        private readonly IApplicationBuilder _app;
        private readonly IWebHostEnvironment _env;

        private bool IsDev => _env.IsDevelopment();
        private bool IsProd => !IsDev;

        public AppConfig(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _app = app;
            _env = env;
        }

        public void Configure()
        {
            ConfigureCookies();
            ConfigureAuth();
            ConfigureCompression();
            ConfigureExceptions();
            ConfigureHttps();
            ConfigureErrors();
            ConfigureCustomHeaders();
            ConfigureCors();
            ConfigureStaticFiles();
            ConfigureMvc();
        }

        private void ConfigureCookies()
        {
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };
            _app.UseCookiePolicy(cookiePolicyOptions);
        }

        private void ConfigureAuth()
        {
            _app.UseAuthentication();
        }

        private void ConfigureCompression()
        {
            _app.UseResponseCompression();
        }

        private void ConfigureExceptions()
        {
            //if (IsDev)
            //    _app.UseDeveloperExceptionPage();
        }

        private void ConfigureHttps()
        {
            if (IsProd)
            {
                _app.UseHsts();
                _app.UseHttpsRedirection();
            }
        }

        private void ConfigureCors()
        {
            _app.UseCors("CorsPolicy");
        }

        private void ConfigureMvc()
        {
            _app.UseMvc(routes => routes.MapRoute("Vue", "{*url}", new { controller = "Vue", action = "Root" }));
        }

        private void ConfigureStaticFiles()
        {
            _app.UseStaticFiles();
        }

        private void ConfigureCustomHeaders()
        {
            _app.UseMiddleware<SecurityHeadersMiddleware>();
        }

        private void ConfigureErrors()
        {
            if (IsDev)
            {
                //var errorUrl = $"/{Routes.Error}";
                //_app.UseStatusCodePagesWithReExecute(errorUrl);
                //_app.UseExceptionHandler(errorUrl);
                //_app.UseMiddleware<ExceptionLoggingMiddleware>();
            }
        }
    }
}