using Core.Entities;
using NUnit.Framework;

namespace Tests.Core
{
	public class GameStatusNameTests
    {
		[Test]
        public void GetName_WithCreatedStatus_IsSetToCreated()
        {
			var result = GameStatusName.GetName(GameStatus.Created);

			Assert.AreEqual("Created", result);
		}

        [Test]
        public void 
		GetName_WithRunningStatus_IsSetToRunning()
        {
			var result = GameStatusName.GetName(GameStatus.Running);

            Assert.AreEqual("Running", result);
		}

		[Test]
        public void GetName_WithFinishedStatus_IsSetToFinished()
        {
			var result = GameStatusName.GetName(GameStatus.Finished);

			Assert.AreEqual("Finished", result);
		}

		[Test]
        public void GetName_WithPublishedStatus_IsSetToPublished()
        {
			var result = GameStatusName.GetName(GameStatus.Published);

			Assert.AreEqual("Published", result);
		}
	}
}