using Core.Exceptions;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AddAppTests
{
    public abstract class Arrange
    {
        protected string AddedAppName;
        private const string GeneratedId = AppData.Id1;

        private const string ValidAppName = AppData.Name1;
        private const string InvalidAppName = "";

        protected AddApp Sut;

        [SetUp]
        public void Setup()
        {
            var arm = new Mock<IAppRepository>();

            arm.Setup(o => o.Add(ValidAppName))
                .Returns(GeneratedId)
                .Callback((string appName) => { AddedAppName = appName; });
            arm.Setup(o => o.Add(InvalidAppName))
                .Throws(new ValidationException("validation-error"));


            Sut = new AddApp(arm.Object);
        }

        protected AddApp.Request ValidRequest => new AddApp.Request(ValidAppName);
        protected AddApp.Request InvalidRequest => new AddApp.Request(InvalidAppName);
    }
}