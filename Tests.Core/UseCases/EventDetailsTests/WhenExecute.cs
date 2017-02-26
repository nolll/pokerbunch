using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EventDetailsTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void EventDetails_NameIsSet()
        {
            Assert.AreEqual(EventData.Name1, Result.Name);
        }
    }
}
