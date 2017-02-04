using System;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.BunchDetailsTests
{
    public abstract class Arrange
    {
        protected abstract Role Role { get; }
    
        protected BunchDetails Sut;
        protected virtual Exception Exception => null;

        [SetUp]
        public void Setup()
        {
            var brm = new Mock<IBunchRepository>();

            if (Exception != null)
            {
                brm.Setup(s => s.Get(BunchData.Id1)).Throws(Exception);
            }
            else
            {
                var bunch = BunchData.Bunch(Role);
                brm.Setup(s => s.Get(BunchData.Id1)).Returns(bunch);
            }

            Sut = new BunchDetails(brm.Object);
        }

        protected BunchDetails.Request Request => new BunchDetails.Request(BunchData.Id1);
    }
}
