using Core.Exceptions;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AddAppTests
{
    public abstract class Arrange : UseCaseTest<AddApp>
    {
        protected string AddedAppName;
        private const string GeneratedId = AppData.Id1;

        protected const string ValidAppName = AppData.Name1;
        protected const string InvalidAppName = "";
        protected abstract string AppName { get; }

        [SetUp]
        public void Setup()
        {
            var arm = new Mock<IAppRepository>();

            arm.Setup(o => o.Add(ValidAppName))
                .Returns(GeneratedId)
                .Callback((string appName) => { AddedAppName = appName; });
            arm.Setup(o => o.Add(InvalidAppName))
                .Throws(new ValidationException("validation-error"));
        }

        protected void Execute() => Sut.Execute(new AddApp.Request(AppName));
    }
}