using Core.Entities;
using NUnit.Framework;

namespace Tests.Core.UseCases.BunchDetailsTests
{
    public class WhenExecuteAsManager : Arrange
    {
        protected override Role Role => Role.Manager;

        [Test]
        public void CanEditIsTrue() => Assert.IsTrue(Result.CanEdit);
    }
}