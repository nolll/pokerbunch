using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Add;
using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Controllers
{
    public class AddPlayerController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Player.Add)]
        public ActionResult Add(string slug)
        {
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Player.Add)]
        public ActionResult Add_Post(string slug, AddPlayerPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new AddPlayer.Request(slug, postModel.Name);
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

            return ShowForm(slug, postModel, errors);
        }

        [Route(WebRoutes.Player.AddConfirmation)]
        public ActionResult Created(string slug)
        {
            var contextResult = GetBunchContext(slug);
            var model = new AddPlayerConfirmationPageModel(contextResult);
            return View(model);
        }

        private ActionResult ShowForm(string slug, AddPlayerPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetBunchContext(slug);
            var model = new AddPlayerPageModel(contextResult, postModel, errors);
            return View(model);
        }
    }
}