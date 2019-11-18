using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Exceptions;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;
using Web.Models.AuthModels;

namespace Web.Controllers
{
    public class LoginController : CoreController
    {
        private readonly Login _login;
        private const int AuthVersion = 3;
        
        public LoginController(AppSettings appSettings, CoreContext coreContext, Login login) 
            : base(appSettings, coreContext)
        {
            _login = login;
        }
        
        [HttpPost]
        [Route("auth/token")]
        public async Task<ActionResult> Token([FromBody]LoginPostModel postModel)
        {
            try
            {
                var request = new Login.Request(postModel.Username, postModel.Password);
                var result = _login.Execute(request);
                await SignIn(result.UserName, result.Token, postModel.RememberMe);
                return JsonView(new JsonTokenViewModel(result.Token));
            }
            catch (LoginException ex)
            {
                return JsonView(new JsonViewModelError(ex.Message));
            }
        }

        private async Task SignIn(string userName, string token, bool isPersistent)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim("Token", token),
                new Claim(ClaimTypes.Version, AuthVersion.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = isPersistent ? DateTimeOffset.UtcNow.AddYears(1) : (DateTimeOffset?)null,
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.UtcNow
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}