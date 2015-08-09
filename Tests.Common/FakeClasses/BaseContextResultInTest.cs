using Core.UseCases;

namespace Tests.Common.FakeClasses
{
    public class BaseContextResultInTest : BaseContext.Result
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