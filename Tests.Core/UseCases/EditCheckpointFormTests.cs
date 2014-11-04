using Core.Urls;
using Core.UseCases.EditCheckpointForm;
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

            var result = Execute(CreateRequest(Constants.ReportCheckpointId));

            Assert.AreEqual(Constants.ReportCheckpointStack, result.Stack);
            Assert.AreEqual(Constants.ReportCheckpointAmount, result.Amount);
            Assert.AreEqual(expected, result.TimeStamp);
        }

        [Test]
        public void EditCheckpointForm_DeleteUrlIsSet()
        {
            var result = Execute(CreateRequest(Constants.ReportCheckpointId));

            Assert.IsInstanceOf<DeleteCheckpointUrl>(result.DeleteUrl);
        }

        [Test]
        public void EditCheckpointForm_CancelUrlIsSet()
        {
            var result = Execute(CreateRequest(Constants.ReportCheckpointId));

            Assert.IsInstanceOf<CashgameActionUrl>(result.CancelUrl);
        }

        [Test]
        public void EditCheckpointForm_WithBuyinCheckpoint_CanEditAmountIsTrue()
        {
            var result = Execute(CreateRequest(Constants.BuyinCheckpointId));

            Assert.IsTrue(result.CanEditAmount);
        }

        [TestCase(Constants.ReportCheckpointId)]
        [TestCase(Constants.CashoutCheckpointId)]
        public void EditCheckpointForm_WithOtherCheckpointType_CanEditAmountIsFalse(int id)
        {
            var result = Execute(CreateRequest(id));

            Assert.IsFalse(result.CanEditAmount);
        }

        private static EditCheckpointFormRequest CreateRequest(int id)
        {
            return new EditCheckpointFormRequest(Constants.SlugB, DateString, PlayerId, id);
        }
        
        private EditCheckpointFormResult Execute(EditCheckpointFormRequest request)
        {
            return EditCheckpointFormInteractor.Execute(
                Repo.Bunch,
                Repo.Checkpoint,
                request);
        }
    }
}
