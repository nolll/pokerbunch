using Core.Entities;
using NUnit.Framework;

namespace Tests.Core.UseCases.UserDetailsTests
{
    public class WhenExecuteAsAdmin : Arrange
    {
        protected override Role Role => Role.Admin;

        [Test]
        public void CanEditIsTrue()
        {
            var result = Sut.Execute(Request);
            Assert.IsTrue(result.CanEdit);
        }
    }
}
