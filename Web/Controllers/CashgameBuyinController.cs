using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.BunchContext;
using Core.UseCases.Buyin;
using Core.UseCases.BuyinForm;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Buyin;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameBuyinController : PokerBunchController
    {
        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/buyin/{playerId:int}")]
        public ActionResult Buyin(string slug, int playerId)
        {
            return ShowForm(slug, playerId);
        }

        [HttpPost]
        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/buyin/{playerId:int}")]
        public ActionResult Buyin_Post(string slug, int playerId, BuyinPostModel postModel)
        {
            var request = new BuyinRequest(slug, playerId, postModel.BuyinAmount, postModel.StackAmount);

            try
            {
                var result = UseCase.Buyin(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(slug, playerId, postModel);
        }

        [HttpPost]
        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/buyinajax/{playerId:int}")]
        public ActionResult Buyin_AjaxPost(string slug, int playerId, BuyinPostModel postModel)
        {
            var request = new BuyinRequest(slug, playerId, postModel.BuyinAmount, postModel.StackAmount);

            try
            {
                UseCase.Buyin(request);
                return Json(new JsonViewModelOk(), JsonRequestBehavior.AllowGet);
            }
            catch (ValidationException ex)
            {
                return Json(new JsonViewModelError(), JsonRequestBehavior.AllowGet);
            }
        }

        private ActionResult ShowForm(string slug, int playerId, BuyinPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var buyinFormResult = UseCase.BuyinForm(new BuyinFormRequest(slug, playerId));
            var model = new BuyinPageModel(contextResult, buyinFormResult, postModel);
            return View("~/Views/Pages/CashgameBuyin/Buyin.cshtml", model);
        }
    }

    public abstract class JsonViewModel
    {
        public virtual bool Success
        {
            get { return false; }
        }
    }

    public class JsonViewModelOk : JsonViewModel
    {
        public override bool Success
        {
            get { return true; }
        }
    }

    public class JsonViewModelError : JsonViewModel
    {
    }
}