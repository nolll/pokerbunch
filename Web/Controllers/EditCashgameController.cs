using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases.BunchContext;
using Core.UseCases.EditCashgame;
using Core.UseCases.EditCashgameForm;
using Web.Commands.CashgameCommands;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Edit;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class EditCashgameController : PokerBunchController
    {
        private readonly ICashgameCommandProvider _cashgameCommandProvider;

        public EditCashgameController(ICashgameCommandProvider cashgameCommandProvider)
        {
            _cashgameCommandProvider = cashgameCommandProvider;
        }

        [AuthorizeManager]
        [Route("{slug}/cashgame/edit/{dateStr}")]
        public ActionResult Edit(string slug, string dateStr)
        {
            return ShowForm(slug, dateStr);
        }

        [HttpPost]
        [AuthorizeManager]
        [Route("{slug}/cashgame/edit/{dateStr}")]
        public ActionResult Post(string slug, string dateStr, EditCashgamePostModel postModel)
        {
            try
            {
                var request = new EditCashgameRequest(slug, dateStr, postModel.Location);
                var result = UseCase.EditCashgame(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(slug, dateStr, postModel);
        }

        private ActionResult ShowForm(string slug, string dateStr, EditCashgamePostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var editCashgameFormResult = UseCase.EditCashgameForm(new EditCashgameFormRequest(slug, dateStr));
            var model = new EditCashgamePageModel(contextResult, editCashgameFormResult, postModel);
            return View("~/Views/Pages/EditCashgame/Edit.cshtml", model);
        }
    }
}