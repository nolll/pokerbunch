using System;
using Core.Exceptions;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases.CashoutTests
{
    public class InvalidStack : Arrange
    {
        [Test]
        public void ThrowsValidationException()
        {
            var request = new Cashout.Request(TestData.UserNameA, TestData.SlugA, TestData.PlayerIdA, -1, DateTime.Now);

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }
    }
}
