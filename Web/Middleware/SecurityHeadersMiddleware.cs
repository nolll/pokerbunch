using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Web.Extensions;
using Web.InlineCode;
using Web.Settings;

namespace Web.Middleware;

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
                    .AddDomain("www.gstatic.com")
                    .AddHash(vueConfigScript.Hash)
            )
            .AddDirective(
                new ContentSecurityDirective("img-src")
                    .AddSelf()
            )
            .AddDirective(
                new ContentSecurityDirective("connect-src")
                    .AddSelf()
                    .AddDomain(apiHost)
            )
            .AddDirective(
                new ContentSecurityDirective("style-src")
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