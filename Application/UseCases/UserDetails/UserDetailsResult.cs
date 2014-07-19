﻿using Application.Urls;
using Core.Entities;

namespace Application.UseCases.UserDetails
{
    public class UserDetailsResult
    {
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string RealName { get; private set; }
        public string Email { get; private set; }
        public string AvatarUrl { get; private set; }
        public bool CanEdit { get; private set; }
        public bool CanChangePassword { get; private set; }
        public Url EditUrl { get; private set; }
        public Url ChangePasswordUrl { get; private set; }

        public UserDetailsResult(User currentUser, User displayUser, string avatarUrl)
        {
            var isViewingCurrentUser = displayUser.UserName == currentUser.UserName;

            UserName = displayUser.UserName;
            DisplayName = displayUser.DisplayName;
            RealName = displayUser.RealName;
            Email = displayUser.Email;
            AvatarUrl = avatarUrl;
            CanEdit = currentUser.IsAdmin || isViewingCurrentUser;
            CanChangePassword = isViewingCurrentUser;
            EditUrl = new EditUserUrl(displayUser.UserName);
            ChangePasswordUrl = new ChangePasswordUrl();
        }
    }
}