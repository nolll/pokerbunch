﻿using Core.UseCases.TestEmail;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class TestEmailTests : TestBase
    {
        private const string Email = "henriks@gmail.com";

        [Test]
        public void TestEmail_MessageIsSent()
        {
            Sut.Execute();

            Assert.AreEqual(Email, Services.MessageSender.To);
            Assert.AreEqual("Test Email", Services.MessageSender.Message.Subject);
            Assert.AreEqual("This is a test email from pokerbunch.com", Services.MessageSender.Message.Body);
        }

        [Test]
        public void TestEmail_EmailIsSet()
        {
            var result = Sut.Execute();

            Assert.AreEqual(Email, result.Email);
        }

        private TestEmailInteractor Sut
        {
            get
            {
                return new TestEmailInteractor(Services.MessageSender);
            }
        }
    }
}
