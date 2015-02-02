using System.Web.Mvc;
using Core.UseCases.BunchContext;
using Core.UseCases.EventList;
using Web.Controllers.Base;
using Web.Models.EventModels.List;

namespace Web.Controllers
{
    public class EventListController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/event/list")]
        public ActionResult List(string slug)
        {
            RequirePlayer(slug);
            var contextResult = UseCase.BunchContext.Execute(new BunchContextRequest(slug));
            var eventListOutput = UseCase.EventList.Execute(new EventListInput(slug));
            var model = new EventListPageModel(contextResult, eventListOutput);
            return View("~/Views/Pages/EventList/EventList.cshtml", model);
        }
    }
}