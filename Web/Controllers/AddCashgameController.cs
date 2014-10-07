using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.AddCashgame;
using Core.UseCases.AddCashgameForm;
using Core.UseCases.BunchContext;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Add;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class AddCashgameController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/cashgame/add")]
        public ActionResult AddCashgame(string slug)
        {
            var model = GetForm(slug);
            return View("AddCashgame/Add", model);
        }

        [HttpPost]
        [AuthorizePlayer]
        [Route("{slug}/cashgame/add")]
        public ActionResult Post(string slug, AddCashgamePostModel postModel)
        {
            var request = new AddCashgameRequest(slug, postModel.Location);

            try
            {
                var result = UseCase.AddCashgame(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            var model = GetForm(slug, postModel);
            return View("AddCashgame/Add", model);
        }

        private AddCashgamePageModel GetForm(string slug, AddCashgamePostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var optionsResult = UseCase.AddCashgameForm(new AddCashgameFormRequest(slug));
            return new AddCashgamePageModel(contextResult, optionsResult, postModel);
        }
    }
}