using Core.Entities;
using NUnit.Framework;

namespace Tests.Core.UseCases.BunchDetailsTests
{
    public class WhenExecuteAsManager : Arrange
    {
        protected override Role Role => Role.Manager;

        [Test]
        public void CanEditIsTrue()
        {
            var result = Sut.Execute(Request);
            Assert.IsTrue(result.CanEdit);
        }
    }
}