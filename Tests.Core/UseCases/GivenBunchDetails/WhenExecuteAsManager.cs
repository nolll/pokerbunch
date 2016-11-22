using Core.Entities;
using NUnit.Framework;

namespace Tests.Core.UseCases.GivenBunchDetails
{
    public class WhenExecuteAsManager : Arrange
    {
        protected override Role Role => Role.Manager;

        [Test]
        public void CanEditIsTrue()
        {
            Assert.IsTrue(Execute().CanEdit);
        }
    }
}