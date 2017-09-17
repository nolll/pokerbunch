﻿using Core.Exceptions;
using Core.Services;

namespace Core.UseCases
{
    public class CoreContext
    {
        private readonly IUserService _userService;

        public CoreContext(IUserService userService)
        {
            _userService = userService;
        }

        public Result Execute(BaseContext.Result baseContext, Request request)
        {
            var isAuthenticated = !string.IsNullOrEmpty(request.UserName);
            var userName = isAuthenticated ? request.UserName : string.Empty;
            var user = isAuthenticated ? _userService.GetByNameOrEmail(request.UserName) : null;
            if (isAuthenticated && user == null) // Broken auth cookie
                throw new NotLoggedInException();
            var userDisplayName = isAuthenticated ? user.DisplayName : string.Empty;
            var isAdmin = isAuthenticated && user.IsAdmin;

            return new Result(
                baseContext,
                isAuthenticated,
                isAdmin,
                userName,
                userDisplayName);
        }

        public class Request
        {
            public string UserName { get; }

            public Request(string userName)
            {
                UserName = userName;
            }
        }

        public class Result
        {
            public bool IsLoggedIn { get; }
            public bool IsAdmin { get; }
            public string UserDisplayName { get; }
            public BaseContext.Result BaseContext { get; }
            public string UserName { get; }

            public Result(
                BaseContext.Result baseContextResult,
                bool isLoggedIn,
                bool isAdmin,
                string userName,
                string userDisplayName)
            {
                BaseContext = baseContextResult;
                IsLoggedIn = isLoggedIn;
                IsAdmin = isAdmin;
                UserDisplayName = userDisplayName;
                UserName = userName;
            }
        }
    }
}