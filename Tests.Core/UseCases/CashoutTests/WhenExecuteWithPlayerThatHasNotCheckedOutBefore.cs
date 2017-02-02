using System.Linq;
using Core.Entities.Checkpoints;
using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashoutTests
{
    public class WhenExecuteWithPlayerThatHasNotCheckedOutBefore : Arrange
    {
        [Test]
        public void AddsCheckpoint()
        {
            Sut.Execute(new Cashout.Request(UserName, BunchId, PlayerId, CashoutStack, CashoutTime));

            Assert.AreEqual(CheckpointCountBeforeCashout + 1, UpdatedCashgame.Checkpoints.Count);
            Assert.IsTrue(UpdatedCashgame.Checkpoints.First(o => o.Type == CheckpointType.Cashout).Stack == CashoutStack);
        }
    }
}
