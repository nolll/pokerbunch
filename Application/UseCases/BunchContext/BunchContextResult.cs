using Application.UseCases.AppContext;

namespace Application.UseCases.BunchContext
{
    public class BunchContextResult : AppContextResult
    {
        public string Slug { get; private set; }
        public string BunchName { get; private set; }
        public int BunchId { get; private set; }
        public bool HasBunch { get; private set; }

        public BunchContextResult(AppContextResult appContextResult)
            : this(appContextResult, null, 0, null)
        {
            HasBunch = false;
        }

        public BunchContextResult(
            AppContextResult appContextResult,
            string slug,
            int bunchId,
            string bunchName)

            : base(
            appContextResult,
            appContextResult.IsLoggedIn,
            appContextResult.IsAdmin,
            appContextResult.UserName,
            appContextResult.UserDisplayName)
        {
            Slug = slug;
            BunchId = bunchId;
            BunchName = bunchName;
            HasBunch = true;
        }
    }
}