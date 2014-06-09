using Application.UseCases.ApplicationContext;

namespace Tests.Common.FakeClasses
{
    public class ApplicationContextResultInTest : ApplicationContextResult
    {
        public ApplicationContextResultInTest(
            bool isLoggedIn = false, 
            bool isAdmin = false, 
            string userName = null, 
            string userDisplayName = null, 
            bool isInProduction = false, 
            string version = null)
            
            : base(
                isLoggedIn, 
                isAdmin, 
                userName, 
                userDisplayName, 
                isInProduction, 
                version)
        {
        }
    }
}