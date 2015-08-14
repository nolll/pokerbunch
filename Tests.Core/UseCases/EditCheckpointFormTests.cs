using Core.Urls;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class EditCheckpointFormTests : TestBase
    {
        private const string DateString = "any";
        private const int PlayerId = 2;

        [Test]
        public void EditCheckpointForm_StackAndAmountAndTimestampIsSet()
        {
            var expected = A.DateTime.AsLocal().Build();

            var result = Sut.Execute(CreateRequest(TestData.ReportCheckpointId));

            Assert.AreEqual(TestData.ReportCheckpointStack, result.Stack);
            Assert.AreEqual(TestData.ReportCheckpointAmount, result.Amount);
            Assert.AreEqual(expected, result.TimeStamp);
        }

        [Test]
        public void EditCheckpointForm_DeleteUrlIsSet()
        {
            var result = Sut.Execute(CreateRequest(TestData.ReportCheckpointId));

            Assert.IsInstanceOf<DeleteCheckpointUrl>(result.DeleteUrl);
        }

        [Test]
        public void EditCheckpointForm_CancelUrlIsSet()
        {
            var result = Sut.Execute(CreateRequest(TestData.ReportCheckpointId));

            Assert.IsInstanceOf<CashgameActionUrl>(result.CancelUrl);
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
            return new EditCheckpointForm.Request(TestData.BunchB.Slug, DateString, PlayerId, id);
        }

        private EditCheckpointForm Sut
        {
            get
            {
                return new EditCheckpointForm(
                    Repos.Bunch,
                    Repos.Checkpoint);
            }
        }
    }
}
