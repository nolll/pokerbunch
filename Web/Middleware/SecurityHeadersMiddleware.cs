using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Web.Extensions;
using Web.InlineCode;
using Web.Settings;

namespace Web.Middleware
{
    [UsedImplicitly]
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        [UsedImplicitly]
        public async Task InvokeAsync(HttpContext httpContext, AppSettings appSettings)
        {
            SetDefaultSecurityHeaders(httpContext);
            SetCspSecurityHeaders(httpContext, appSettings);
            await _next(httpContext);
        }

        private static void SetDefaultSecurityHeaders(HttpContext httpContext)
        {
            httpContext.AddHeader("X-Content-Type-Options", "nosniff");
            httpContext.AddHeader("X-Frame-Options", "DENY");
            httpContext.AddHeader("X-XSS-Protection", "1; mode=block");
            httpContext.AddHeader("Strict-Transport-Security", "max-age=63072000; includeSubDomains");
            httpContext.AddHeader("Access-Control-Allow-Origin", "*");
        }

        private static void SetCspSecurityHeaders(HttpContext httpContext, AppSettings appSettings)
        {
            var csp = GetCsp(appSettings);
            httpContext.AddHeader("Content-Security-Policy", csp.ToString());
        }

        private static ContentSecurityPolicy GetCsp(AppSettings appSettings)
        {
            var gaScript = new GoogleAnalyticsScript();
            var fontStyle = new FontStyle();
            var vueConfigScript = new VueConfigScript(appSettings);
            var apiHost = appSettings.Urls.ApiHost;

            var csp = new ContentSecurityPolicy()
                .AddDirective(
                    new ContentSecurityDirective("default-src")
                        .AddSelf()
                )
                .AddDirective(
                    new ContentSecurityDirective("script-src")
                        .AddSelf()
                        .AddDomain("*.google-analytics.com")
                        .AddDomain("www.gstatic.com")
                        .AddHash(gaScript.Hash)
                        .AddHash(vueConfigScript.Hash)
                )
                .AddDirective(
                    new ContentSecurityDirective("img-src")
                        .AddSelf()
                        .AddDomain("*.google-analytics.com")
                )
                .AddDirective(
                    new ContentSecurityDirective("connect-src")
                        .AddSelf()
                        .AddDomain("*.google-analytics.com")
                        .AddDomain(apiHost)
                )
                .AddDirective(
                    new ContentSecurityDirective("style-src-elem")
                        .AddSelf()
                        .AddDomain("www.gstatic.com")
                        .AddDomain("fonts.googleapis.com")
                        .AddHash(fontStyle.Hash)
                )
                .AddDirective(
                    new ContentSecurityDirective("font-src")
                        .AddSelf()
                        .AddDomain("fonts.gstatic.com")
                );

            return csp;
        }
    }

    public class ContentSecurityPolicy
    {
        private readonly List<ContentSecurityDirective> _directives;

        public ContentSecurityPolicy()
        {
            _directives = new List<ContentSecurityDirective>();
        }

        public ContentSecurityPolicy AddDirective(ContentSecurityDirective directive)
        {
            _directives.Add(directive);
            return this;
        }

        public override string ToString()
        {
            return string.Join("; ", _directives.Select(o => o.ToString()));
        }
    }

    public class ContentSecurityDirective
    {
        private readonly string _name;
        private readonly List<string> _values;

        public ContentSecurityDirective(string name)
        {
            _name = name;
            _values = new List<string>();
        }

        public ContentSecurityDirective AddSelf()
        {
            _values.Add("'self'");
            return this;
        }

        public ContentSecurityDirective AddDomain(string domain)
        {
            _values.Add(domain);
            return this;
        }

        public ContentSecurityDirective AddHash(string hash)
        {
            _values.Add($"'sha256-{hash}'");
            return this;
        }

        public override string ToString()
        {
            var values = string.Join(" ", _values);
            return $"{_name} {values}";
        }
    }
}