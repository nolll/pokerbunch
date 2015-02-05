using Core.Exceptions;
using Core.UseCases.RequirePlayer;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class RequirePlayerTests : TestBase
    {
        [Test]
        public void RequirePlayer_WithGuest_AccessIsDenied()
        {
            var request = new RequirePlayerRequest(Constants.SlugA, Constants.UserNameD);
            Assert.Throws<AccessDeniedException>(() => Sut.Execute(request));
        }

        [Test]
        public void RequirePlayer_WithPlayer_AccessGranted()
        {
            var request = new RequirePlayerRequest(Constants.SlugA, Constants.UserNameA);
            Assert.DoesNotThrow(() => Sut.Execute(request));
        }

        [Test]
        public void RequirePlayer_WithManager_AccessGranted()
        {
            var request = new RequirePlayerRequest(Constants.SlugA, Constants.UserNameC);
            Assert.DoesNotThrow(() => Sut.Execute(request));
        }

        [Test]
        public void RequirePlayer_WithAdmin_AccessGranted()
        {
            var request = new RequirePlayerRequest(Constants.SlugA, Constants.UserNameB);
            Assert.DoesNotThrow(() => Sut.Execute(request));
        }

        private RequirePlayerInteractor Sut
        {
            get
            {
                return new RequirePlayerInteractor(
                    Repos.Bunch,
                    Repos.User,
                    Repos.Player);
            }
        }
    }
}