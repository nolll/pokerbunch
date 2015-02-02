using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.AddCashgame;
using Core.UseCases.AddCashgameForm;
using Core.UseCases.BunchContext;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Add;

namespace Web.Controllers
{
    public class AddCashgameController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/cashgame/add")]
        public ActionResult AddCashgame(string slug)
        {
            RequirePlayer(slug);
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route("{slug}/cashgame/add")]
        public ActionResult Post(string slug, AddCashgamePostModel postModel)
        {
            var request = new AddCashgameRequest(slug, postModel.Location);

            try
            {
                var result = UseCase.AddCashgame.Execute(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(slug, postModel);
        }

        private ActionResult ShowForm(string slug, AddCashgamePostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext.Execute(new BunchContextRequest(slug));
            var optionsResult = UseCase.AddCashgameForm.Execute(new AddCashgameFormRequest(slug));
            var model = new AddCashgamePageModel(contextResult, optionsResult, postModel);
            return View("~/Views/Pages/AddCashgame/Add.cshtml", model);
        }
    }
}