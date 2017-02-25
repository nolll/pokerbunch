using Core.Entities;
using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerListTests
{
    public class WithManagerRole : Arrange
    {
        protected override Role Role => Role.Manager;

        [Test]
        public void CanAddPlayerIsTrue() => Assert.IsTrue(Result.CanAddPlayer);
    }
}