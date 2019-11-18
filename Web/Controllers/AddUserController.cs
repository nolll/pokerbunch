using System.Collections.Generic;
using Core.Exceptions;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.UserModels.Add;

namespace Web.Controllers
{
    public class AddUserController : CoreController
    {
        private readonly AddUser _addUser;

        public AddUserController(AppSettings appSettings, CoreContext coreContext, AddUser addUser) 
            : base(appSettings, coreContext)
        {
            _addUser = addUser;
        }

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
                _addUser.Execute(request);
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
            var model = new AddUserPageModel(AppSettings, contextResult, postModel, errors);
            return View(model);
        }

        [Route(AddUserConfirmationUrl.Route)]
        public ActionResult Done()
        {
            var contextResult = GetAppContext();
            var model = new AddUserConfirmationPageModel(AppSettings, contextResult);
            return View(model);
        }
    }
}