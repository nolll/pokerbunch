using System;
using Core.Exceptions;
using NUnit.Framework;

namespace Tests.Core.UseCases.BunchDetailsTests
{
    public class WithNoRole : Arrange
    {
        protected override Exception Exception => new AccessDeniedException();

        [Test]
        public void AccessDenied()
        {
            Assert.Throws<AccessDeniedException>(() => Execute());
        }
    }
}
