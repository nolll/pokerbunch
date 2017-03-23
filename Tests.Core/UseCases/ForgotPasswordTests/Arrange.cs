using Core.Repositories;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.ForgotPasswordTests
{
    public abstract class Arrange : UseCaseTest<ForgotPassword>
    {
        protected const string Email = UserData.Email1;

        protected string PostedEmail;

        protected override void Setup()
        {
            PostedEmail = null;

            Mock<IUserRepository>().Setup(o => o.ResetPassword(It.IsAny<string>())).Callback((string email) => PostedEmail = email);
        }

        protected override void Execute()
        {
            Subject.Execute(new ForgotPassword.Request(Email));
        }
    }
}