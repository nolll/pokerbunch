using Core.Exceptions;
using Core.Repositories;
using Core.UseCases;
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
            Mock<IAppRepository>().Setup(o => o.Add(ValidAppName)).Returns(GeneratedId)
                .Callback((string appName) => { AddedAppName = appName; });
            Mock<IAppRepository>().Setup(o => o.Add(InvalidAppName))
                .Throws(new ValidationException("validation-error"));
        }

        protected void Execute() => Sut.Execute(new AddApp.Request(AppName));
    }
}