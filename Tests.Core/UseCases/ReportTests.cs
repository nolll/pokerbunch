using System;
using Core.Exceptions;
using Core.UseCases.Report;
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

            var request = new ReportRequest(Constants.SlugA, Constants.PlayerIdA, -1, DateTime.Now);

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Cashout_PlayerHasNotCheckedOutBefore_AddsCheckpoint(int stack)
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new ReportRequest(Constants.SlugA, Constants.PlayerIdA, stack, DateTime.Now);
            Sut.Execute(request);

            Assert.AreEqual(stack, Repos.Checkpoint.Added.Stack);
        }

        private ReportInteractor Sut
        {
            get
            {
                return new ReportInteractor(
                    Repos.Bunch,
                    Repos.Cashgame,
                    Repos.Player,
                    Repos.Checkpoint);
            }
        }
    }
}