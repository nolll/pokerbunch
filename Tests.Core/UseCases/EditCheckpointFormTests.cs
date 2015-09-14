using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class EditCheckpointFormTests : TestBase
    {
        [Test]
        public void EditCheckpointForm_StackAndAmountAndTimestampIsSet()
        {
            var expected = A.DateTime.AsUtc().Build();

            var result = Sut.Execute(CreateRequest(TestData.ReportCheckpointId));

            Assert.AreEqual(TestData.ReportCheckpointStack, result.Stack);
            Assert.AreEqual(TestData.ReportCheckpointAmount, result.Amount);
            Assert.AreEqual(expected, result.TimeStamp);
        }

        [Test]
        public void EditCheckpointForm_CheckpointIdIsSet()
        {
            var result = Sut.Execute(CreateRequest(TestData.ReportCheckpointId));

            Assert.AreEqual(2, result.CheckpointId);
        }

        [Test]
        public void EditCheckpointForm_CancelUrlIsSet()
        {
            var result = Sut.Execute(CreateRequest(TestData.ReportCheckpointId));

            Assert.AreEqual(1, result.CashgameId);
            Assert.AreEqual(3, result.PlayerId);
        }

        [Test]
        public void EditCheckpointForm_WithBuyinCheckpoint_CanEditAmountIsTrue()
        {
            var result = Sut.Execute(CreateRequest(TestData.BuyinCheckpointId));

            Assert.IsTrue(result.CanEditAmount);
        }

        [TestCase(TestData.ReportCheckpointId)]
        [TestCase(TestData.CashoutCheckpointId)]
        public void EditCheckpointForm_WithOtherCheckpointType_CanEditAmountIsFalse(int id)
        {
            var result = Sut.Execute(CreateRequest(id));

            Assert.IsFalse(result.CanEditAmount);
        }

        private static EditCheckpointForm.Request CreateRequest(int id)
        {
            return new EditCheckpointForm.Request(TestData.ManagerUser.UserName, id);
        }

        private EditCheckpointForm Sut
        {
            get
            {
                return new EditCheckpointForm(
                    Services.BunchService,
                    Services.CashgameService,
                    Services.UserService,
                    Services.PlayerService);
            }
        }
    }
}
