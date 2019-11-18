using Core.Settings;
using Core.UseCases;

namespace Web.Controllers.Base
{
    public abstract class BunchController : CoreController
    {
        private readonly BunchContext _bunchContext;

        protected BunchController(AppSettings appSettings, CoreContext coreContext, BunchContext bunchContext)
            : base(appSettings, coreContext)
        {
            _bunchContext = bunchContext;
        }

        protected BunchContext.Result GetBunchContext(string bunchId)
        {
            return _bunchContext.Execute(GetAppContext(), new BunchContext.Request(bunchId));
        }
    }
}