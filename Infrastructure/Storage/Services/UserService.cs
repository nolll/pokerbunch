using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using JetBrains.Annotations;

namespace Infrastructure.Storage.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly ApiConnection _api;

        public UserService(ApiConnection api)
        {
            _api = api;
        }

        public User Current()
        {
            var apiUser = _api.Get<ApiUser>(Url.User());
            return CreateUser(apiUser);
        }

        public User GetById(string id)
        {
            var apiUser = _api.Get<ApiUser>(Url.User(id));
            return CreateUser(apiUser);
        }

        public IList<User> List()
        {
            var apiUsers = _api.Get<IList<ApiUser>>(Url.Bunches);
            return apiUsers.Select(CreateUser).ToList();
        }

        public User GetByNameOrEmail(string nameOrEmail)
        {
            var apiUser = _api.Get<ApiUser>(Url.UserByName(nameOrEmail));
            return CreateUser(apiUser);
        }

        public void Update(User user)
        {
            var id = user.Id;
            var postUser = new ApiUser(user);
            _api.Post<ApiUser>(Url.User(id), postUser);
        }

        public string Add(User user, string password)
        {
            var postUser = new ApiUser(user, password);
            var apiUser = _api.Post<ApiUser>(Url.Users, postUser);
            return apiUser.Id;
        }

        public void ChangePassword(string oldPassword, string newPassword, string repeat)
        {
            var apiChangePassword = new ApiChangePassword(oldPassword, newPassword, repeat);
            _api.Post(Url.ChangePassword, apiChangePassword);
        }

        public void ResetPassword(string email)
        {
            var apiResetPassword = new ApiResetPassword(email);
            _api.Post(Url.ResetPassword, apiResetPassword);
        }

        private User CreateUser(ApiUser u)
        {
            return new User(u.Id, u.UserName, u.DisplayName, u.RealName, u.Email, u.GlobalRole);
        }

        private class ApiUser
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public string UserName { get; set; }
            [UsedImplicitly]
            public string DisplayName { get; set; }
            [UsedImplicitly]
            public string RealName { get; set; }
            [UsedImplicitly]
            public string Email { get; set; }
            [UsedImplicitly]
            public Role GlobalRole { get; set; }
            [UsedImplicitly]
            public string Password { get; set; }

            public ApiUser(User user, string password = null)
            {
                Id = user.Id;
                UserName = user.UserName;
                DisplayName = user.DisplayName;
                RealName = user.RealName;
                Email = user.Email;
                GlobalRole = user.GlobalRole;
                Password = password;
            }

            public ApiUser()
            {
            }
        }

        public class ApiChangePassword
        {
            [UsedImplicitly]
            public string OldPassword { get; set; }
            [UsedImplicitly]
            public string NewPassword { get; set; }
            [UsedImplicitly]
            public string Repeat { get; set; }

            public ApiChangePassword(string oldPassword, string newPassword, string repeat)
            {
                OldPassword = oldPassword;
                NewPassword = newPassword;
                Repeat = repeat;
            }
        }

        public class ApiResetPassword
        {
            [UsedImplicitly]
            public string Email { get; set; }

            public ApiResetPassword(string email)
            {
                Email = email;
            }
        }
    }
}