using Core.Entities;
using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CoreContextTests
{
    public abstract class Arrange : UseCaseTest<CoreContext>
    {
        protected CoreContext.Result Result;

        private const string UserId = UserData.Id1;
        protected const string DisplayName = UserData.DisplayName1;
        protected abstract string UserName { get; }

        protected override void Setup()
        {
            Mock<IUserService>().Setup(o => o.Current()).Returns(new User(UserId, UserName, DisplayName));
        }

        protected override void Execute()
        {
            var baseContext = new BaseContext.Result("1");
            Result = Subject.Execute(baseContext, new CoreContext.Request(UserName));
        }
    }
}