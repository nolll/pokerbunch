using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Add;
using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Controllers
{
    public class AddCashgameController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.Add)]
        public ActionResult AddCashgame(string slug)
        {
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Cashgame.Add)]
        public ActionResult Post(string slug, AddCashgamePostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new AddCashgame.Request(slug, postModel.LocationId, postModel.EventId);
                var result = UseCase.AddCashgame.Execute(request);
                return Redirect(new RunningCashgameUrl(result.Slug).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }

            return ShowForm(slug, postModel, errors);
        }

        private ActionResult ShowForm(string slug, AddCashgamePostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetBunchContext(slug);
            var optionsResult = UseCase.AddCashgameForm.Execute(new AddCashgameForm.Request(slug));
            var model = new AddCashgamePageModel(contextResult, optionsResult, postModel, errors);
            return View(model);
        }
    }
}