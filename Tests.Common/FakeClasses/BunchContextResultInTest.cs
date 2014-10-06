using Core.UseCases.AppContext;
using Core.UseCases.BunchContext;

namespace Tests.Common.FakeClasses
{
    public class BunchContextResultInTest : BunchContextResult
    {
        public BunchContextResultInTest(
            AppContextResult appContextResult = null,
            string slug = null, 
            int bunchId = default(int), 
            string bunchName = null)
            
            : base(
                appContextResult ?? new AppContextResultInTest(),
                slug,
                bunchId,
                bunchName)
        {
        }
    }
}