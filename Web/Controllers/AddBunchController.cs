using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Routes;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Add;

namespace Web.Controllers
{
    public class AddBunchController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Bunch.Add)]
        public ActionResult Add()
        {
            return ShowForm();
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Bunch.Add)]
        public ActionResult Add_Post(AddBunchPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new AddBunch.Request(postModel.DisplayName, postModel.Description, postModel.CurrencySymbol, postModel.CurrencyLayout, postModel.TimeZone);
                UseCase.AddBunch.Execute(request);
                return Redirect(new AddBunchConfirmationUrl().Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }
            catch (BunchExistsException ex)
            {
                errors.Add(ex.Message);
            }

            return ShowForm(postModel, errors);
        }

        [Route(WebRoutes.Bunch.AddConfirmation)]
        public ActionResult Created()
        {
            var contextResult = GetAppContext();
            var model = new AddBunchConfirmationPageModel(contextResult);
            return View(model);
        }

        private ActionResult ShowForm(AddBunchPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetAppContext();
            var bunchFormResult = UseCase.AddBunchForm.Execute();
            var model = new AddBunchPageModel(contextResult, bunchFormResult, postModel, errors);
            return View(model);
        }
    }
}