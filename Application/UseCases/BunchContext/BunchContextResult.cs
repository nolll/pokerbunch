using Application.UseCases.ApplicationContext;

namespace Application.UseCases.BunchContext
{
    public class BunchContextResult : AppContextResult
    {
        public string Slug { get; private set; }
        public string BunchName { get; private set; }
        public int BunchId { get; private set; }

        public BunchContextResult(
            AppContextResult appContextResult,
            string slug,
            int bunchId,
            string bunchName)

            : this(
            appContextResult.IsLoggedIn,
            appContextResult.IsAdmin,
            appContextResult.UserName,
            appContextResult.UserDisplayName,
            appContextResult.IsInProduction,
            appContextResult.Version,
            slug,
            bunchId,
            bunchName)
        {
            Slug = slug;
            BunchId = bunchId;
            BunchName = bunchName;
        }

        protected BunchContextResult(
            bool isLoggedIn,
            bool isAdmin,
            string userName,
            string userDisplayName,
            bool isInProduction,
            string version,
            string slug,
            int bunchId,
            string bunchName)

            : base(
                isLoggedIn,
                isAdmin,
                userName,
                userDisplayName,
                isInProduction,
                version)
        {
            Slug = slug;
            BunchId = bunchId;
            BunchName = bunchName;
        }
    }
}