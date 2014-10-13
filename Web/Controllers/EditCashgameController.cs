using System.Web.Mvc;
using Core.Urls;
using Web.Commands.CashgameCommands;
using Web.Controllers.Base;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.Models.CashgameModels.Edit;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class EditCashgameController : PokerBunchController
    {
        private readonly IEditCashgamePageBuilder _editCashgamePageBuilder;
        private readonly ICashgameCommandProvider _cashgameCommandProvider;

        public EditCashgameController(IEditCashgamePageBuilder editCashgamePageBuilder, ICashgameCommandProvider cashgameCommandProvider)
        {
            _editCashgamePageBuilder = editCashgamePageBuilder;
            _cashgameCommandProvider = cashgameCommandProvider;
        }

        [AuthorizeManager]
        [Route("{slug}/cashgame/edit/{dateStr}")]
        public ActionResult Edit(string slug, string dateStr)
        {
            var model = _editCashgamePageBuilder.Build(slug, dateStr);
            return View("~/Views/Pages/EditCashgame/Edit.cshtml", model);
        }

        [HttpPost]
        [AuthorizeManager]
        [Route("{slug}/cashgame/edit/{dateStr}")]
        public ActionResult Edit_Post(string slug, string dateStr, CashgameEditPostModel postModel)
        {
            var command = _cashgameCommandProvider.GetEditCommand(slug, dateStr, postModel);
            if (command.Execute())
            {
                return Redirect(new CashgameDetailsUrl(slug, dateStr).Relative);
            }
            AddModelErrors(command.Errors);
            var model = _editCashgamePageBuilder.Build(slug, dateStr, postModel);
            return View("~/Views/Pages/EditCashgame/Edit.cshtml", model);
        } 
    }
}