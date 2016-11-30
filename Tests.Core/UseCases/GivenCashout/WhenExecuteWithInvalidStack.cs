﻿using Core.Exceptions;
using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.GivenCashout
{
    public class WhenExecuteWithInvalidStack : Arrange
    {
        protected override int CashoutStack => -1;

        [Test]
        public void ThrowsValidationException()
        {
            var request = new Cashout.Request(UserName, BunchId, PlayerId, CashoutStack, CashoutTime);

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }
    }
}