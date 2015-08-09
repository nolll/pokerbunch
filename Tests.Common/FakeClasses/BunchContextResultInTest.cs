using Core.Entities;
using Core.UseCases;

namespace Tests.Common.FakeClasses
{
    public class BunchContextResultInTest : BunchContext.Result
    {
        public BunchContextResultInTest(
            AppContext.Result appContextResult = null,
            string slug = null, 
            int bunchId = default(int), 
            string bunchName = null,
            Role userRole = Role.None,
            int userPlayerId = default(int))
            
            : base(
                appContextResult ?? new AppContextResultInTest(),
                slug,
                bunchId,
                bunchName,
                userRole,
                userPlayerId)
        {
        }
    }
}