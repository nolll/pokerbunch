using System.Globalization;
using System.Web;
using System.Web.Security;
using Core;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Services.Interfaces;
using Newtonsoft.Json;

namespace Infrastructure.Web
{
    public class Auth : IAuth
    {
        private readonly ITimeProvider _timeProvider;
        private readonly IUserRepository _userRepository;
        private const int Version = 1;

        public Auth(
            ITimeProvider timeProvider,
            IUserRepository userRepository)
        {
            _timeProvider = timeProvider;
            _userRepository = userRepository;
        }

        public void SignIn(UserIdentity user, bool createPersistentCookie)
        {
            var userData = JsonConvert.SerializeObject(user);

            var currentTime = _timeProvider.UtcNow;
            var expires = currentTime.AddYears(100);

            var authTicket = new FormsAuthenticationTicket(
                Version,
                user.UserId.ToString(CultureInfo.InvariantCulture),
                currentTime,
                expires,
                createPersistentCookie,
                userData);

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
            {
                Expires = authTicket.Expiration,
                Path = FormsAuthentication.FormsCookiePath
            };

            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        private CustomIdentity GetIdentity()
        {
            var identity = HttpContext.Current.User.Identity as CustomIdentity;
            return identity ?? new CustomIdentity();
        }

        public User CurrentUser
        {
            get
            {
                var identity = GetIdentity();
                return identity.IsAuthenticated ? _userRepository.GetById(identity.UserId) : null;
            }
        }

        public bool IsInRole(string slug, Role role)
        {
            var identity = GetIdentity();
            return identity.IsInRole(slug, role);
        }
    }
}