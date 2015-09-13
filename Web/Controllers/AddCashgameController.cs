using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Add;
using Web.Urls;

namespace Web.Controllers
{
    public class AddCashgameController : BaseController
    {
        [Authorize]
        [Route(Routes.CashgameAdd)]
        public ActionResult AddCashgame(string slug)
        {
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route(Routes.CashgameAdd)]
        public ActionResult Post(string slug, AddCashgamePostModel postModel)
        {
            var request = new AddCashgame.Request(CurrentUserName, slug, postModel.Location);

            try
            {
                var result = UseCase.AddCashgame.Execute(request);
                return Redirect(new RunningCashgameUrl(result.Slug).Relative);
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
            var optionsResult = UseCase.AddCashgameForm.Execute(new AddCashgameForm.Request(CurrentUserName, slug));
            var model = new AddCashgamePageModel(contextResult, optionsResult, postModel);
            return View("~/Views/Pages/AddCashgame/Add.cshtml", model);
        }
    }
}