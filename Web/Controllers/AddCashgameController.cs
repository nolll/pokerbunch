using System.Collections.Generic;
using Core.Exceptions;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Add;

namespace Web.Controllers
{
    public class AddCashgameController : BunchController
    {
        private readonly AddCashgame _addCashgame;
        private readonly AddCashgameForm _addCashgameForm;

        public AddCashgameController(
            AppSettings appSettings, 
            CoreContext coreContext, 
            BunchContext bunchContext, 
            AddCashgame addCashgame, 
            AddCashgameForm addCashgameForm)
            : base(
                appSettings, 
                coreContext, 
                bunchContext)
        {
            _addCashgame = addCashgame;
            _addCashgameForm = addCashgameForm;
        }

        [Authorize]
        [Route(AddCashgameUrl.Route)]
        public ActionResult AddCashgame(string bunchId)
        {
            return ShowForm(bunchId);
        }

        [HttpPost]
        [Authorize]
        [Route(AddCashgameUrl.Route)]
        public ActionResult Post(string bunchId, AddCashgamePostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new AddCashgame.Request(bunchId, postModel.LocationId);
                var result = _addCashgame.Execute(request);
                return Redirect(new CashgameDetailsUrl(result.Slug, result.CashgameId).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }

            return ShowForm(bunchId, postModel, errors);
        }

        private ActionResult ShowForm(string bunchId, AddCashgamePostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetBunchContext(bunchId);
            var optionsResult = _addCashgameForm.Execute(new AddCashgameForm.Request(bunchId));
            var model = new AddCashgamePageModel(AppSettings, contextResult, optionsResult, postModel, errors);
            return View(model);
        }
    }
}