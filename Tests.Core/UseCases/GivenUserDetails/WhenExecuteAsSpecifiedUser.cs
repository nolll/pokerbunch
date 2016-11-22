using NUnit.Framework;

namespace Tests.Core.UseCases.GivenUserDetails
{
    public class WhenExecuteAsSpecifiedUser : Arrange
    {
        protected override bool ViewingOwnUser => true;

        [Test]
        public void CanEditIsTrue()
        {
            Assert.IsTrue(Result.CanEdit);
        }

        [Test]
        public void CanChangePasswordIsTrue()
        {
            Assert.IsTrue(Result.CanChangePassword);
        }
    }
}
