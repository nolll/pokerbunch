using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Edit;

namespace Web.Controllers
{
    public class EditCashgameController : BaseController
    {
        [Authorize]
        [Route(EditCashgameUrl.Route)]
        public ActionResult Edit(string cashgameId)
        {
            return ShowForm(cashgameId);
        }

        [HttpPost]
        [Authorize]
        [Route(EditCashgameUrl.Route)]
        public ActionResult Post(string cashgameId, EditCashgamePostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new EditCashgame.Request(cashgameId, postModel.LocationId, postModel.EventId);
                var result = UseCase.EditCashgame.Execute(request);
                return Redirect(new CashgameDetailsUrl(result.BunchId, result.CashgameId).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }

            return ShowForm(cashgameId, postModel, errors);
        }

        private ActionResult ShowForm(string cashgameId, EditCashgamePostModel postModel = null, IEnumerable<string> errors = null)
        {
            var editCashgameFormResult = UseCase.EditCashgameForm.Execute(new EditCashgameForm.Request(cashgameId));
            var contextResult = GetBunchContext(editCashgameFormResult.Slug);
            var model = new EditCashgamePageModel(contextResult, editCashgameFormResult, postModel, errors);
            return View(model);
        }
    }
}