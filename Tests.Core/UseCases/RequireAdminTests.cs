using Core.Exceptions;
using Core.UseCases.RequireManager;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class RequireAdminTests : TestBase
    {
        [Test]
        public void RequireAdmin_WithNonAdmin_AccessIsDenied()
        {
            var request = new RequireAdminRequest(Constants.UserNameD);
            Assert.Throws<AccessDeniedException>(() => Sut.Execute(request));
        }

        [Test]
        public void RequireAdmin_WithAdmin_AccessGranted()
        {
            var request = new RequireAdminRequest(Constants.UserNameB);
            Assert.DoesNotThrow(() => Sut.Execute(request));
        }

        private RequireAdminInteractor Sut
        {
            get
            {
                return new RequireAdminInteractor(
                    Repos.User);
            }
        }
    }
}