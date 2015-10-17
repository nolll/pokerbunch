﻿using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Web.Common.Services;

namespace Api.Extensions
{
    public class CustomRequireHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var request = actionContext.Request;

            if (RequestEvaluator.IsTestEnvironment(request))
                return;

            if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                HttpResponseMessage response;
                var uri = new UriBuilder(request.RequestUri)
                {
                    Scheme = Uri.UriSchemeHttps, 
                    Port = 443
                };
                var body = string.Format("<p>The resource can be found at <a href=\"{0}\">{0}</a>.</p>", uri.Uri.AbsoluteUri);
                if (request.Method.Equals(HttpMethod.Get) || request.Method.Equals(HttpMethod.Head))
                {
                    response = request.CreateResponse(HttpStatusCode.Found);
                    response.Headers.Location = uri.Uri;
                    if (request.Method.Equals(HttpMethod.Get))
                    {
                        response.Content = new StringContent(body, Encoding.UTF8, "text/html");
                    }
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound);
                    response.Content = new StringContent(body, Encoding.UTF8, "text/html");
                }

                actionContext.Response = response;
            }
        }
    }
}