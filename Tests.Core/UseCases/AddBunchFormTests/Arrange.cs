using Core.UseCases;

namespace Tests.Core.UseCases.AddBunchFormTests
{
    public class Arrange : UseCaseTest<AddBunchForm>
    {
        protected AddBunchForm.Result Result;

        protected override void Setup()
        {
        }

        protected override void Execute()
        {
            Result = Subject.Execute();
        }
    }
}