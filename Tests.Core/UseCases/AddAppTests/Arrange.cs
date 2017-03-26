using Core.Exceptions;
using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AddAppTests
{
    public abstract class Arrange : UseCaseTest<AddApp>
    {
        protected override bool ExecuteAutomatically => false;
        protected string AddedAppName;
        private const string GeneratedId = AppData.Id1;

        protected const string ValidAppName = AppData.Name1;
        protected const string InvalidAppName = "";
        protected abstract string AppName { get; }

        protected override void Setup()
        {
            Mock<IAppService>().Setup(o => o.Add(ValidAppName)).Returns(GeneratedId)
                .Callback((string appName) => { AddedAppName = appName; });
            Mock<IAppService>().Setup(o => o.Add(InvalidAppName))
                .Throws(new ValidationException("validation-error"));
        }

        protected override void Execute()
        {
            Subject.Execute(new AddApp.Request(AppName));
        }
    }
}