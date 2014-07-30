using Application.UseCases.BaseContext;

namespace Tests.Common.FakeClasses
{
    public class BaseContextResultInTest : BaseContextResult
    {
        public BaseContextResultInTest(
            bool isInProduction = false,
            string version = null)
            
            : base(
                isInProduction,
                version)
        {
        }
    }
}