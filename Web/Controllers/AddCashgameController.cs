using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Add;

namespace Web.Controllers
{
    public class AddCashgameController : BaseController
    {
        [Authorize]
        [Route("{slug}/cashgame/add")]
        public ActionResult AddCashgame(string slug)
        {
            var context = GetBunchContext(slug);
            RequirePlayer(context);
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route("{slug}/cashgame/add")]
        public ActionResult Post(string slug, AddCashgamePostModel postModel)
        {
            var request = new AddCashgame.Request(slug, postModel.Location);

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
            var contextResult = GetBunchContext(slug);
            var optionsResult = UseCase.AddCashgameForm.Execute(new AddCashgameForm.Request(slug));
            var model = new AddCashgamePageModel(contextResult, optionsResult, postModel);
            return View("~/Views/Pages/AddCashgame/Add.cshtml", model);
        }
    }
}