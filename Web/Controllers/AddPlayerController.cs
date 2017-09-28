using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Add;

namespace Web.Controllers
{
    public class AddPlayerController : BaseController
    {
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
                var result = UseCase.AddPlayer.Execute(request);
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
            var model = new AddPlayerConfirmationPageModel(contextResult);
            return View(model);
        }

        private ActionResult ShowForm(string bunchId, AddPlayerPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetBunchContext(bunchId);
            var model = new AddPlayerPageModel(contextResult, postModel, errors);
            return View(model);
        }
    }
}