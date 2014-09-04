﻿using System;
using System.Web.Mvc;
using Application.UseCases.AppContext;
using Application.UseCases.EditUserForm;
using Application.UseCases.UserDetails;
using Application.UseCases.UserList;
using NUnit.Framework;
using Tests.Common;
using Web.Commands.UserCommands;
using Web.Controllers;

namespace Tests.Web.ControllerTests
{
    class UserControllerTests : MockContainer
    {
        [Test]
        public void List_RequiresAdmin()
        {
            Func<ActionResult> methodToTest = Sut.List;
            var result = SecurityTestHelper.RequiresAdmin(methodToTest);

            Assert.IsTrue(result);
        }

        private UserController Sut
        {
            get
            {
                return new UserController(
                    GetMock<IAppContextInteractor>().Object,
                    GetMock<IUserDetailsInteractor>().Object,
                    GetMock<IUserListInteractor>().Object,
                    GetMock<IUserCommandProvider>().Object,
                    GetMock<IEditUserFormInteractor>().Object);
            }
        }
    }
}