using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Urls;
using Core.UseCases.EditCheckpointForm;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class EditCheckpointFormTests : TestBase
    {
        private const string DateString = "any";
        private const int CheckpointId = 1;
        private const int PlayerId = 2;
        private const int Stack = 3;
        private const int Amount = 4;

        [Test]
        public void EditCheckpointForm_StackIsSet()
        {
            var checkpoint = A.Checkpoint.WithStack(Stack).Build();
            SetupCheckpoint(checkpoint);

            var result = Execute(CreateRequest());

            Assert.AreEqual(Stack, result.Stack);
        }

        [Test]
        public void EditCheckpointForm_AmountIsSet()
        {
            var checkpoint = A.Checkpoint.WithAmount(Amount).Build();
            SetupCheckpoint(checkpoint);

            var result = Execute(CreateRequest());

            Assert.AreEqual(Amount, result.Amount);
        }

        [Test]
        public void EditCheckpointForm_TimestampIsSet()
        {
            var timestamp = A.DateTime.AsUtc().Build();
            var expected = A.DateTime.AsLocal().Build();
            var checkpoint = A.Checkpoint.WithTimestamp(timestamp).Build();
            SetupCheckpoint(checkpoint);

            var result = Execute(CreateRequest());

            Assert.AreEqual(expected, result.TimeStamp);
        }

        [Test]
        public void EditCheckpointForm_DeleteUrlIsSet()
        {
            var checkpoint = A.Checkpoint.Build();
            SetupCheckpoint(checkpoint);

            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<DeleteCheckpointUrl>(result.DeleteUrl);
        }

        [Test]
        public void EditCheckpointForm_CancelUrlIsSet()
        {
            var checkpoint = A.Checkpoint.Build();
            SetupCheckpoint(checkpoint);

            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<CashgameActionUrl>(result.CancelUrl);
        }

        [Test]
        public void EditCheckpointForm_WithBuyinCheckpoint_CanEditAmountIsTrue()
        {
            var checkpoint = A.Checkpoint.OfType(CheckpointType.Buyin).Build();
            SetupCheckpoint(checkpoint);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.CanEditAmount);
        }

        [TestCase(CheckpointType.Report)]
        [TestCase(CheckpointType.Cashout)]
        public void EditCheckpointForm_WithOtherCheckpointType_CanEditAmountIsFalse(CheckpointType type)
        {
            var checkpoint = A.Checkpoint.OfType(type).Build();
            SetupCheckpoint(checkpoint);

            var result = Execute(CreateRequest());

            Assert.IsFalse(result.CanEditAmount);
        }

        private static EditCheckpointFormRequest CreateRequest()
        {
            return new EditCheckpointFormRequest(Constants.SlugB, DateString, PlayerId, CheckpointId);
        }

        private void SetupCheckpoint(Checkpoint checkpoint)
        {
            GetMock<ICheckpointRepository>().Setup(o => o.GetCheckpoint(CheckpointId)).Returns(checkpoint);
        }
        
        private EditCheckpointFormResult Execute(EditCheckpointFormRequest request)
        {
            return EditCheckpointFormInteractor.Execute(
                Repo.Bunch,
                GetMock<ICheckpointRepository>().Object,
                request);
        }
    }
}
