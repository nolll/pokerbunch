using System;
using System.Web.Mvc;
using Application.UseCases.AddBunchForm;
using Application.UseCases.AppContext;
using Application.UseCases.BunchContext;
using Application.UseCases.BunchDetails;
using Application.UseCases.BunchList;
using Application.UseCases.EditBunchForm;
using Application.UseCases.JoinBunchConfirmation;
using Application.UseCases.JoinBunchForm;
using NUnit.Framework;
using Tests.Common;
using Web.Commands.HomegameCommands;
using Web.Controllers;

namespace Tests.Web.ControllerTests
{
    class HomegameControllerTests : MockContainer
    {
        [Test]
        public void List_RequiresAdmin()
        {
            Func<ActionResult> methodToTest = Sut.List;
            var result = SecurityTestHelper.RequiresAdmin(methodToTest);

            Assert.IsTrue(result);
        }

        private HomegameController Sut
        {
            get
            {
                return new HomegameController(
                    GetMock<IAppContextInteractor>().Object,
                    GetMock<IBunchContextInteractor>().Object,
                    GetMock<IBunchListInteractor>().Object,
                    GetMock<IAddBunchFormInteractor>().Object,
                    GetMock<IJoinBunchFormInteractor>().Object,
                    GetMock<IJoinBunchConfirmationInteractor>().Object,
                    GetMock<IBunchDetailsInteractor>().Object,
                    GetMock<IEditBunchFormInteractor>().Object,
                    GetMock<IBunchCommandProvider>().Object);
            }
        }
    }
}
