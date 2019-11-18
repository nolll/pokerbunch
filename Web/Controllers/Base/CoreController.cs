using Core.Settings;
using Core.UseCases;

namespace Web.Controllers.Base
{
    public abstract class CoreController : BaseController
    {
        private readonly CoreContext _coreContext;

        protected CoreController(AppSettings appSettings, CoreContext coreContext)
            : base(appSettings)
        {
            _coreContext = coreContext;
        }

        protected CoreContext.Result GetAppContext()
        {
            return _coreContext.Execute(new CoreContext.Request(Identity.UserName));
        }
    }
}