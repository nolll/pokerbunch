using System.Diagnostics;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Owin;
using Web;

[assembly: OwinStartup(typeof(Startup))]
namespace Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.HubPipeline.AddModule(new ErrorHandlingPipelineModule());
            app.MapSignalR();
        }
    }

    public class ErrorHandlingPipelineModule : HubPipelineModule
    {
        protected override void OnIncomingError(ExceptionContext exceptionContext, IHubIncomingInvokerContext invokerContext)
        {
            Debug.WriteLine("=> Exception " + exceptionContext.Error.Message);
            if (exceptionContext.Error.InnerException != null)
            {
                Debug.WriteLine("=> Inner Exception " + exceptionContext.Error.InnerException.Message);
            }
            base.OnIncomingError(exceptionContext, invokerContext);

        }
    }
}