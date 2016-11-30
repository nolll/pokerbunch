﻿using Core.Exceptions;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class CoreContext
    {
        private readonly IUserRepository _userRepository;

        public CoreContext(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Result Execute(Request request)
        {
            var isAuthenticated = !string.IsNullOrEmpty(request.UserName);
            var userName = isAuthenticated ? request.UserName : string.Empty;
            var user = isAuthenticated ? _userRepository.GetByNameOrEmail(userName) : null;
            if (isAuthenticated && user == null) // Broken auth cookie
                throw new NotLoggedInException();
            var userId = isAuthenticated ? user.Id : "";
            var userDisplayName = isAuthenticated ? user.DisplayName : string.Empty;
            var isAdmin = isAuthenticated && user.IsAdmin;
            var baseContextResult = new BaseContext().Execute();

            return new Result(
                baseContextResult,
                isAuthenticated,
                isAdmin,
                userId,
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
            public bool IsLoggedIn { get; private set; }
            public bool IsAdmin { get; private set; }
            public string UserId { get; private set; }
            public string UserDisplayName { get; private set; }
            public BaseContext.Result BaseContext { get; private set; }
            public string UserName { get; private set; }

            public Result(
                BaseContext.Result baseContextResult,
                bool isLoggedIn,
                bool isAdmin,
                string userId,
                string userName,
                string userDisplayName)
            {
                BaseContext = baseContextResult;
                IsLoggedIn = isLoggedIn;
                IsAdmin = isAdmin;
                UserId = userId;
                UserDisplayName = userDisplayName;
                UserName = userName;
            }
        }
    }
}