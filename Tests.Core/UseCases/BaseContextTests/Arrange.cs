using Core.UseCases;

namespace Tests.Core.UseCases.BaseContextTests
{
    public abstract class Arrange : UseCaseTest<BaseContext>
    {
        protected BaseContext.Result Result;

        protected override void Setup()
        {
        }

        protected override void Execute()
        {
            Result = Subject.Execute();
        }
    }
}