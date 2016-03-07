using Core.Entities;
using Core.Services;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases.BunchDetailsTests
{
    public abstract class Arrange : ArrangeBase
    {
        private const int BunchId = 1;
        private const int UserId = 4;
        private const int PlayerId = 5;
        private const string Slug = "slug";
        private const string UserName = "username";
        protected const string DisplayName = "displayname";
        protected const string Description = "description";
        protected const string HouseRules = "houserules";
        protected virtual Role Role => Role.Player;
        protected BunchDetails.Result Result;

        [SetUp]
        public void Setup()
        {
            var sut = CreateSut<BunchDetails>();

            MockOf<IBunchService>().Setup(s => s.GetBySlug(Slug)).Returns(new Bunch(BunchId, Slug, DisplayName, Description, HouseRules));
            MockOf<IPlayerService>().Setup(s => s.GetByUserId(BunchId, UserId)).Returns(new Player(BunchId, PlayerId, UserId, role: Role));
            MockOf<IUserService>().Setup(s => s.GetByNameOrEmail(UserName)).Returns(new User(UserId, UserName));

            Result = sut.Execute(new BunchDetails.Request(UserName, Slug));
        }
    }
}
