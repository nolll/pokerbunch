using System;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;

namespace Tests.Core.UseCases.BunchDetailsTests
{
    public abstract class Arrange
    {
        private const string BunchId = "1";
        private const string UserId = "4";
        private const string PlayerId = "5";
        private const string Slug = "slug";
        private const string UserName = "username";
        protected const string DisplayName = "displayname";
        protected const string Description = "description";
        protected const string HouseRules = "houserules";
        protected virtual Role Role => Role.None;
        protected BunchDetails Sut;
        protected virtual Exception Exception => null;

        [SetUp]
        public void Setup()
        {
            var brm = new Mock<IBunchRepository>();

            if (Exception != null)
            {
                brm.Setup(s => s.Get(Slug)).Throws(Exception);
            }
            else
            {
                brm.Setup(s => s.Get(Slug)).Returns(new Bunch(Slug, DisplayName, Description, HouseRules, null, 0, null, Role));
            }

            Sut = new BunchDetails(brm.Object);
        }

        protected BunchDetails.Request Request => new BunchDetails.Request(UserName, Slug);
    }
}
