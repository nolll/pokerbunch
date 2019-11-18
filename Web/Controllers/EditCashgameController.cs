using System.Collections.Generic;
using Core.Exceptions;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Edit;

namespace Web.Controllers
{
    public class EditCashgameController : BunchController
    {
        private readonly EditCashgame _editCashgame;
        private readonly EditCashgameForm _editCashgameForm;

        public EditCashgameController(AppSettings appSettings, CoreContext coreContext, BunchContext bunchContext, EditCashgame editCashgame, EditCashgameForm editCashgameForm) 
            : base(appSettings, coreContext, bunchContext)
        {
            _editCashgame = editCashgame;
            _editCashgameForm = editCashgameForm;
        }

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
                var result = _editCashgame.Execute(request);
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
            var editCashgameFormResult = _editCashgameForm.Execute(new EditCashgameForm.Request(cashgameId));
            var contextResult = GetBunchContext(editCashgameFormResult.Slug);
            var model = new EditCashgamePageModel(AppSettings, contextResult, editCashgameFormResult, postModel, errors);
            return View(model);
        }
    }
}