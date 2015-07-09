using System.Web.Mvc;
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
            var contextResult = GetBunchContext(slug);
            var eventListOutput = UseCase.EventList.Execute(new EventListInput(slug));
            var model = new EventListPageModel(contextResult, eventListOutput);
            return View("~/Views/Pages/EventList/EventList.cshtml", model);
        }
    }
}