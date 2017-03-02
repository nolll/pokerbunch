using System.Linq;
using NUnit.Framework;

namespace Tests.Core.UseCases.BaseContextTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void VersionIsSet()
        {
            var numberOfDots = Result.Version.Count(o => o == '.');

            Assert.AreEqual(2, numberOfDots);
        }
    }
}