﻿using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Web.Extensions;
using Web.Services;
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
            httpContext.AddHeader("Content-Security-Policy", csp);
        }

        private static string GetCsp(AppSettings appSettings)
        {
            return string.Join("; ", GetDefaultCspValues(appSettings));
        }

        private static IEnumerable<string> GetDefaultCspValues(AppSettings appSettings)
        {
            var vueConfigHash = VueConfigScriptService.ComputedSha256Hash(appSettings);
            var apiHost = appSettings.Urls.ApiHost;

            return new List<string>
            {
                "default-src 'self'",
                $"script-src 'self' *.google-analytics.com 'sha256-{GaScriptService.ComputedSha256Hash}' 'sha256-{vueConfigHash}'",
                "img-src 'self' *.google-analytics.com",
                $"connect-src 'self' *.google-analytics.com {apiHost}",
                $"style-src-elem 'self' fonts.googleapis.com 'sha256-{FontStyleService.ComputedSha256Hash}'",
                "font-src 'self' fonts.gstatic.com"
            };
        }
    }
}