using Core.Entities;
using NUnit.Framework;

namespace Tests.Core.UseCases.GivenUserDetails
{
    public class WhenExecuteAsAdmin : Arrange
    {
        protected override Role Role => Role.Admin;

        [Test]
        public void CanEditIsTrue()
        {
            Assert.IsTrue(Result.CanEdit);
        }
    }
}
