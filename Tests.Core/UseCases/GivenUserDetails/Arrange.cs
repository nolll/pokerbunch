﻿using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases.GivenUserDetails
{
    public class Arrange : ArrangeBase
    {
        private const string CurrentUserId = "1";
        private const string ViewUserId = "2";
        private string _currentUserName = "currentusername";
        protected const string ViewUserName = "viewusername";
        protected const string DisplayName = "displayname";
        protected const string RealName = "realname";
        protected const string Email = "email";
        protected virtual Role Role => Role.Player;
        protected virtual bool ViewingOwnUser => false; 
        protected UserDetails.Result Result;

        [SetUp]
        public void Setup()
        {
            var sut = CreateSut<UserDetails>();

            if (ViewingOwnUser)
            {
                MockOf<IUserRepository>().Setup(s => s.GetByNameOrEmail(ViewUserName)).Returns(new User(ViewUserId, ViewUserName, DisplayName, RealName, Email, Role));
                _currentUserName = ViewUserName;
            }
            else
            {
                MockOf<IUserRepository>().Setup(s => s.GetByNameOrEmail(_currentUserName)).Returns(new User(CurrentUserId, _currentUserName, globalRole: Role));
                MockOf<IUserRepository>().Setup(s => s.GetByNameOrEmail(ViewUserName)).Returns(new User(ViewUserId, ViewUserName, DisplayName, RealName, Email, Role));
            }

            Result = sut.Execute(new UserDetails.Request(_currentUserName, ViewUserName));
        }
    }
}