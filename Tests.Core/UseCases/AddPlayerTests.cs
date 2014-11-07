using System.Linq;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases.AddPlayer;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class AddPlayerTests : TestBase
    {
        private const string EmptyName = "";
        private const string UniqueName = "Unique Name";
        private const string ExistingName = Constants.PlayerNameA;

        [Test]
        public void AddPlayer_ReturnUrlIsSet()
        {
            var request = new AddPlayerRequest(Constants.SlugA, UniqueName);
            var result = Execute(request);

            Assert.IsInstanceOf<AddPlayerConfirmationUrl>(result.ReturnUrl);
        }

        [Test]
        public void AddPlayer_EmptyName_ThrowsException()
        {
            var request = new AddPlayerRequest(Constants.SlugA, EmptyName);

            var ex = Assert.Throws<ValidationException>(() => Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        [Test]
        public void AddPlayer_ValidName_AddsPlayer()
        {
            var request = new AddPlayerRequest(Constants.SlugA, UniqueName);
            Execute(request);

            Assert.IsNotNull(Repo.Player.Added);
        }

        [Test]
        public void AddPlayer_ValidNameButNameExists_ThrowsException()
        {
            var request = new AddPlayerRequest(Constants.SlugA, ExistingName);
            Assert.Throws<PlayerExistsException>(() => Execute(request));
        }

        private AddPlayerResult Execute(AddPlayerRequest request)
        {
            return AddPlayerInteractor.Execute(
                Repo.Bunch,
                Repo.Player,
                request);
        }
    }
}
