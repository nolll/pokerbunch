using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Add;

namespace Web.Controllers
{
    public class AddCashgameController : BaseController
    {
        [Authorize]
        [Route(AddCashgameUrl.Route)]
        public ActionResult AddCashgame(string bunchId)
        {
            return ShowForm(bunchId);
        }

        [HttpPost]
        [Authorize]
        [Route(AddCashgameUrl.Route)]
        public ActionResult Post(string bunchId, AddCashgamePostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new AddCashgame.Request(bunchId, postModel.LocationId, postModel.EventId);
                var result = UseCase.AddCashgame.Execute(request);
                return Redirect(new RunningCashgameUrl(result.Slug).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }

            return ShowForm(bunchId, postModel, errors);
        }

        private ActionResult ShowForm(string bunchId, AddCashgamePostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetBunchContext(bunchId);
            var optionsResult = UseCase.AddCashgameForm.Execute(new AddCashgameForm.Request(bunchId));
            var model = new AddCashgamePageModel(contextResult, optionsResult, postModel, errors);
            return View(model);
        }
    }
}