using Core.Exceptions;
using Core.UseCases.RequireManager;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class RequireManagerTests : TestBase
    {
        [Test]
        public void RequireManager_WithGuest_AccessDenied()
        {
            var request = new RequireManagerRequest(Constants.SlugA, Constants.UserNameD);
            Assert.Throws<AccessDeniedException>(() => Sut.Execute(request));
        }

        [Test]
        public void RequireManager_WithPlayer_AccessDenied()
        {
            var request = new RequireManagerRequest(Constants.SlugA, Constants.UserNameA);
            Assert.Throws<AccessDeniedException>(() => Sut.Execute(request));
        }

        [Test]
        public void RequireManager_WithManager_AccessGranted()
        {
            var request = new RequireManagerRequest(Constants.SlugA, Constants.UserNameC);
            Assert.DoesNotThrow(() => Sut.Execute(request));
        }

        [Test]
        public void RequireManager_WithAdmin_AccessGranted()
        {
            var request = new RequireManagerRequest(Constants.SlugA, Constants.UserNameB);
            Assert.DoesNotThrow(() => Sut.Execute(request));
        }

        private RequireManagerInteractor Sut
        {
            get
            {
                return new RequireManagerInteractor(
                    Repos.Bunch,
                    Repos.User,
                    Repos.Player);
            }
        }
    }
}