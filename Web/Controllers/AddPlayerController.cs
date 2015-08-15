using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Add;

namespace Web.Controllers
{
    public class AddPlayerController : BaseController
    {
        [Authorize]
        [Route(Routes.PlayerAdd)]
        public ActionResult Add(string slug)
        {
            var context = GetBunchContext(slug);
            RequireManager(context);
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route(Routes.PlayerAdd)]
        public ActionResult Add_Post(string slug, AddPlayerPostModel postModel)
        {
            var context = GetBunchContext(slug);
            RequireManager(context);
            var request = new AddPlayer.Request(slug, postModel.Name);

            try
            {
                var result = UseCase.AddPlayer.Execute(request);
                return Redirect(result.ReturnUrl.Relative);
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

        [Route(Routes.PlayerAddConfirmation)]
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