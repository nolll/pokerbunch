using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Add;

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
            var contextResult = GetBunchContextBySlug(slug);
            var optionsResult = UseCase.AddCashgameForm.Execute(new AddCashgameForm.Request(CurrentUserName, slug));
            var model = new AddCashgamePageModel(contextResult, optionsResult, postModel);
            return View("~/Views/Pages/AddCashgame/Add.cshtml", model);
        }
    }
}