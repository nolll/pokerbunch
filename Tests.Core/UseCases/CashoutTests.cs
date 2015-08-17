using System;
using Core.Exceptions;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class CashoutTests : TestBase
    {
        [Test]
        public void Cashout_InvalidStack_ThrowsValidationException()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new Cashout.Request(TestData.UserNameA, TestData.SlugA, TestData.PlayerIdA, -1, DateTime.Now);

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Cashout_PlayerHasNotCheckedOutBefore_AddsCheckpoint(int stack)
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new Cashout.Request(TestData.UserNameA, TestData.SlugA, TestData.PlayerIdA, stack, DateTime.Now);
            Sut.Execute(request);

            Assert.AreEqual(stack, Repos.Checkpoint.Added.Stack);
        }

        [Test]
        public void Cashout_PlayerHasCheckedOutBefore_UpdatesCheckpoint()
        {
            Repos.Cashgame.SetupRunningGameWithCashoutCheckpoint();

            var request2 = new Cashout.Request(TestData.UserNameA, TestData.SlugA, TestData.PlayerIdA, 2, DateTime.Now.AddMinutes(1));
            Sut.Execute(request2);

            Assert.AreEqual(2, Repos.Checkpoint.Saved.Stack);
        }

        private Cashout Sut
        {
            get
            {
                return new Cashout(
                    Repos.Bunch,
                    Repos.Cashgame,
                    Repos.Player,
                    Repos.Checkpoint,
                    Repos.User);
            }
        }
    }
}