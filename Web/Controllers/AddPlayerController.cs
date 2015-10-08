using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Common.Routes;
using Web.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Add;

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
            try
            {
                var request = new AddPlayer.Request(CurrentUserName, slug, postModel.Name);
                var result = UseCase.AddPlayer.Execute(request);
                return Redirect(new AddPlayerConfirmationUrl(result.Slug).Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }
            catch (PlayerExistsException ex)
            {
                AddModelError(ex.Message);
            }

            return ShowForm(slug, postModel);
        }

        [Route(WebRoutes.Player.AddConfirmation)]
        public ActionResult Created(string slug)
        {
            var contextResult = GetBunchContext(slug);
            var model = new AddPlayerConfirmationPageModel(contextResult);
            return View("~/Views/Pages/AddPlayer/AddConfirmation.cshtml", model);
        }

        private ActionResult ShowForm(string slug, AddPlayerPostModel postModel = null)
        {
            var contextResult = GetBunchContext(slug);
            var model = new AddPlayerPageModel(contextResult, postModel);
            return View("~/Views/Pages/AddPlayer/Add.cshtml", model);
        }
    }
}