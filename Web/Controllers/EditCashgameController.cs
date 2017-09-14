using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Edit;
using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Controllers
{
    public class EditCashgameController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.Edit)]
        public ActionResult Edit(string id)
        {
            return ShowForm(id);
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Cashgame.Edit)]
        public ActionResult Post(string id, EditCashgamePostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new EditCashgame.Request(id, postModel.LocationId, postModel.EventId);
                var result = UseCase.EditCashgame.Execute(request);
                return Redirect(new CashgameDetailsUrl(result.CashgameId).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }

            return ShowForm(id, postModel, errors);
        }

        private ActionResult ShowForm(string id, EditCashgamePostModel postModel = null, IEnumerable<string> errors = null)
        {
            var editCashgameFormResult = UseCase.EditCashgameForm.Execute(new EditCashgameForm.Request(id));
            var contextResult = GetBunchContext(editCashgameFormResult.Slug);
            var model = new EditCashgamePageModel(contextResult, editCashgameFormResult, postModel, errors);
            return View(model);
        }
    }
}