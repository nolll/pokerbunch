using Application.UseCases.BunchContext;

namespace Tests.Common.FakeClasses
{
    public class BunchContextResultInTest : BunchContextResult
    {
        public BunchContextResultInTest(
            bool isLoggedIn = false, 
            bool isAdmin = false, 
            string userName = null, 
            string userDisplayName = null, 
            bool isInProduction = false, 
            string version = null,
            string slug = null, 
            int bunchId = default(int), 
            string bunchName = null)
            
            : base(
                isLoggedIn,
                isAdmin,
                userName,
                userDisplayName,
                isInProduction,
                version,
                slug,
                bunchId,
                bunchName)
        {
        }
    }
}