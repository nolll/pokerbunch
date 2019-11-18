using System.Collections.Generic;
using Core.Exceptions;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Add;

namespace Web.Controllers
{
    public class AddPlayerController : BunchController
    {
        private readonly AddPlayer _addPlayer;

        public AddPlayerController(AppSettings appSettings, CoreContext coreContext, BunchContext bunchContext, AddPlayer addPlayer) 
            : base(appSettings, coreContext, bunchContext)
        {
            _addPlayer = addPlayer;
        }

        [Authorize]
        [Route(AddPlayerUrl.Route)]
        public ActionResult Add(string bunchId)
        {
            return ShowForm(bunchId);
        }

        [HttpPost]
        [Authorize]
        [Route(AddPlayerUrl.Route)]
        public ActionResult Add_Post(string bunchId, AddPlayerPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new AddPlayer.Request(bunchId, postModel.Name);
                var result = _addPlayer.Execute(request);
                return Redirect(new AddPlayerConfirmationUrl(result.Slug).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }
            catch (PlayerExistsException ex)
            {
                errors.Add(ex.Message);
            }

            return ShowForm(bunchId, postModel, errors);
        }

        [Route(AddPlayerConfirmationUrl.Route)]
        public ActionResult Created(string bunchId)
        {
            var contextResult = GetBunchContext(bunchId);
            var model = new AddPlayerConfirmationPageModel(AppSettings, contextResult);
            return View(model);
        }

        private ActionResult ShowForm(string bunchId, AddPlayerPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetBunchContext(bunchId);
            var model = new AddPlayerPageModel(AppSettings, contextResult, postModel, errors);
            return View(model);
        }
    }
}