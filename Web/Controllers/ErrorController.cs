using System;
using System.Web.Mvc;
using Application.UseCases.AppContext;
using Application.UseCases.BaseContext;
using Web.Models.PageBaseModels;

namespace Web.Controllers
{
    [HandleError]
    public class ErrorController : Controller
    {
        private readonly IBaseContextInteractor _contextInteractor;

        public ErrorController(IBaseContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public ActionResult NotFound()
        {
            var contextResult = _contextInteractor.Execute();

            var model = new Error404PageModel(contextResult);
            return View("Error", model);
        }

        public ActionResult ServerError()
        {
            var contextResult = _contextInteractor.Execute();

            var exception = Server.GetLastError();
            var message = exception != null ? exception.Message : "Unknown error";

            var model = new Error500PageModel(contextResult, message);
            return View("Error", model);
        }
    }

    public class Error404PageModel : ErrorPageModel
    {
        public Error404PageModel(BaseContextResult contextResult)
            : base(contextResult, "Page not found", "Please check the url for errors")
        {
        }
    }

    public class Error500PageModel : ErrorPageModel
    {
        public Error500PageModel(BaseContextResult contextResult, string message)
            : base(contextResult, "Server Error", message)
        {
        }
    }

    public class ErrorPageModel : PageModel
    {
        public string Title { get; private set; }
        public string Message { get; private set; }

        protected ErrorPageModel(BaseContextResult contextResult, string title, string message)
            : base("Error", contextResult)
        {
            Title = title;
            Message = message;
        }
    }
}
