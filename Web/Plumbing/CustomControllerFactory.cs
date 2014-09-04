using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Plumbing;

namespace Web.Plumbing
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        private readonly DependencyContainer _deps;

        public CustomControllerFactory(DependencyContainer deps)
        {
            _deps = deps;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            return (IController) Activator.CreateInstance(controllerType, new object[]{_deps});
        }
    }
}