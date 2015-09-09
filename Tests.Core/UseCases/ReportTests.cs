using System;
using Core.Exceptions;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class ReportTests : TestBase
    {
        [Test]
        public void Cashout_InvalidStack_ThrowsValidationException()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new Report.Request(TestData.UserNameA, TestData.SlugA, TestData.PlayerIdA, -1, DateTime.Now);

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Cashout_PlayerHasNotCheckedOutBefore_AddsCheckpoint(int stack)
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new Report.Request(TestData.UserNameA, TestData.SlugA, TestData.PlayerIdA, stack, DateTime.Now);
            Sut.Execute(request);

            Assert.AreEqual(stack, Repos.Checkpoint.Added.Stack);
        }

        private Report Sut
        {
            get
            {
                return new Report(
                    Services.BunchService,
                    Repos.Cashgame,
                    Repos.Player,
                    Repos.Checkpoint,
                    Services.UserService);
            }
        }
    }
}