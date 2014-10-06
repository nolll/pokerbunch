using Core.UseCases.AppContext;

namespace Core.UseCases.BunchContext
{
    public class BunchContextResult
    {
        public string Slug { get; private set; }
        public string BunchName { get; private set; }
        public int BunchId { get; private set; }
        public bool HasBunch { get; private set; }
        public AppContextResult AppContext { get; private set; }

        public BunchContextResult(AppContextResult appContextResult)
        {
            AppContext = appContextResult;
        }

        public BunchContextResult(
            AppContextResult appContextResult,
            string slug,
            int bunchId,
            string bunchName)
            : this(appContextResult)
        {
            Slug = slug;
            BunchId = bunchId;
            BunchName = bunchName;
            HasBunch = true;
        }
    }
}