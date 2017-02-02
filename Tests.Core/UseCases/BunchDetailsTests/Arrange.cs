using System;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases.BunchDetailsTests
{
    public abstract class Arrange : ArrangeBase
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
        private BunchDetails.Request _request;
        private BunchDetails _sut;
        protected virtual Exception Exception => null;

        [SetUp]
        public void Setup()
        {
            var bunchRepoMock = new Mock<IBunchRepository>();

            if (Exception != null)
            {
                bunchRepoMock.Setup(s => s.Get(Slug)).Throws(Exception);
            }
            else
            {
                bunchRepoMock.Setup(s => s.Get(Slug)).Returns(new Bunch(Slug, DisplayName, Description, HouseRules, null, 0, null, Role));
            }

            _sut = new BunchDetails(bunchRepoMock.Object);
        }

        protected BunchDetails.Result Execute()
        {
            _request = new BunchDetails.Request(UserName, Slug);
            return _sut.Execute(_request);
        }
    }
}
