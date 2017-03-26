using Core.Services;
using Core.UseCases;
using Moq;

namespace Tests.Core.UseCases.ChangePasswordTests
{
    public abstract class Arrange : UseCaseTest<ChangePassword>
    {
        protected const string OldPassword = "old-password";
        protected const string NewPassword = "new-password";
        protected const string Repeat = "repeat";

        protected string PostedOldPassword;
        protected string PostedNewPassword;
        protected string PostedRepeat;

        protected override void Setup()
        {
            PostedOldPassword = null;
            PostedNewPassword = null;
            PostedRepeat = null;

            Mock<IUserService>().Setup(o => o.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Callback((string oldPassword, string newPassword, string repeat) => { PostedOldPassword = oldPassword; PostedNewPassword = newPassword; PostedRepeat = repeat; });
        }

        protected override void Execute()
        {
            Subject.Execute(new ChangePassword.Request(OldPassword, NewPassword, Repeat));
        }
    }
}