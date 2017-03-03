using NUnit.Framework;
namespace Tests.Core.UseCases.AddEventTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void EventIsAdded() => Assert.AreEqual(EventName, Added.Name);

        [Test]
        public void SlugIsSet() => Assert.AreEqual(BunchId, Result.Slug);
    }
}