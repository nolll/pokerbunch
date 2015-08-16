using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.EventModels.List;

namespace Web.Controllers
{
    public class EventListController : BaseController
    {
        [Authorize]
        [Route(Routes.EventList)]
        public ActionResult List(string slug)
        {
            var contextResult = GetBunchContextBySlug(slug);
            RequirePlayer(contextResult);
            var eventListOutput = UseCase.EventList.Execute(new EventList.Request(CurrentUserName, slug));
            var model = new EventListPageModel(contextResult, eventListOutput);
            return View("~/Views/Pages/EventList/EventList.cshtml", model);
        }
    }
}