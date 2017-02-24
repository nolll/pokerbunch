using Core.Entities;
using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerListTests
{
    public class WithManagerRole : Arrange
    {
        protected override Role Role => Role.Manager;

        [Test]
        public void Execute_PlayerIsManager_CanAddPlayerIsTrue()
        {
            var result = Sut.Execute(Request);

            Assert.IsTrue(result.CanAddPlayer);
        }
    }
}