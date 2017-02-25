using Core.Entities;
using NUnit.Framework;

namespace Tests.Core.UseCases.ActionTests
{
    public class WithManager : Arrange
    {
        protected override Role Role => Role.Manager;

        [Test]
        public void CanEditIsTrueOnItem() => Assert.IsTrue(Result.CheckpointItems[0].CanEdit);
    }
}