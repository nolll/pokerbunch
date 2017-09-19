using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.UserModels.Add;
using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Controllers
{
    public class AddUserController : BaseController
    {
        [Route(AddUserUrl.Route)]
        public ActionResult AddUser()
        {
            return ShowForm();
        }

        [HttpPost]
        [Route(AddUserUrl.Route)]
        public ActionResult Post(AddUserPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new AddUser.Request(postModel.UserName, postModel.DisplayName, postModel.Email, postModel.Password);
                UseCase.AddUser.Execute(request);
                return Redirect(new AddUserConfirmationUrl().Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }
            catch (UserExistsException ex)
            {
                errors.Add(ex.Message);
            }
            catch (EmailExistsException ex)
            {
                errors.Add(ex.Message);
            }
            
            return ShowForm(postModel, errors);
        }

        private ActionResult ShowForm(AddUserPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetAppContext();
            var model = new AddUserPageModel(contextResult, postModel, errors);
            return View(model);
        }

        [Route(AddUserConfirmationUrl.Route)]
        public ActionResult Done()
        {
            var contextResult = GetAppContext();
            var model = new AddUserConfirmationPageModel(contextResult);
            return View(model);
        }
    }
}