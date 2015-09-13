﻿using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class JoinBunchConfirmationTests : TestBase
    {
        [Test]
        public void JoinBunchConfirmation_BunchNameIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(TestData.BunchA.DisplayName, result.BunchName);
        }

        [Test]
        public void JoinBunchConfirmation_BunchDetailsUrlIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual("bunch-a", result.Slug);
        }

        private static JoinBunchConfirmation.Request CreateRequest()
        {
            return new JoinBunchConfirmation.Request(TestData.UserNameA, TestData.SlugA);
        }

        private JoinBunchConfirmation Sut
        {
            get
            {
                return new JoinBunchConfirmation(
                    Services.BunchService,
                    Services.UserService,
                    Services.PlayerService);
            }
        }
    }
}
