using Application.UseCases.ApplicationContext;

namespace Tests.Common.FakeClasses
{
    public class ApplicationContextResultInTest : ApplicationContextResult
    {
        public ApplicationContextResultInTest(
            bool isLoggedIn = false, 
            bool isAdmin = false, 
            string userName = "", 
            string userDisplayName = "", 
            bool isInProduction = false, 
            string version = "")
            
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