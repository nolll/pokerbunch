using System;
using System.Web;
using System.Web.Security;
using Core;
using Core.Entities;
using Core.Services;

namespace Infrastructure.Web
{
    public class Auth : IAuth
    {
        private CustomIdentity CurrentIdentity
        {
            get
            {
                var identity = HttpContext.Current.User.Identity as CustomIdentity;
                return identity ?? new CustomIdentity();
            }
        }

        public bool IsInRole(string slug, Role role)
        {
            return CurrentIdentity.IsInRole(slug, role);
        }
    }
}