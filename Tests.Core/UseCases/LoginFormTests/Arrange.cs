using Core.UseCases;

namespace Tests.Core.UseCases.LoginFormTests
{
    public class Arrange : UseCaseTest<LoginForm>
    {
        protected LoginForm.Result Result;

        protected const string HomeUrl = "/a";
        private const string EmptyReturnUrl = "";
        protected const string ExistingReturnUrl = "/b";
        protected virtual string ReturnUrl => EmptyReturnUrl; 

        protected override void Setup()
        {
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new LoginForm.Request(HomeUrl, ReturnUrl));
        }
    }
}