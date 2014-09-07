﻿using Application.Services;
using Core.Repositories;

namespace Application.UseCases.UserDetails
{
    public static class UserDetailsInteractor
    {
        public static UserDetailsResult Execute(IAuth auth, IUserRepository userRepository, UserDetailsRequest request)
        {
            var currentUser = auth.CurrentUser;
            var displayUser = userRepository.GetByNameOrEmail(request.UserName);

            return new UserDetailsResult(currentUser, displayUser);
        }
    }
}