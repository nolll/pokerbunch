using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Routes;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.LocationModels.Add;

namespace Web.Controllers
{
    public class AddLocationController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Location.Add)]
        public ActionResult Add(string slug)
        {
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Location.Add)]
        public ActionResult Add_Post(string slug, AddLocationPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new AddLocation.Request(slug, postModel.Name);
                var result = UseCase.AddLocation.Execute(request);
                return Redirect(new AddLocationConfirmationUrl(result.Slug).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }

            return ShowForm(slug, postModel, errors);
        }

        [Route(WebRoutes.Location.AddConfirmation)]
        public ActionResult Created(string slug)
        {
            var contextResult = GetBunchContext(slug);
            var model = new AddLocationConfirmationPageModel(contextResult);
            return View(model);
        }

        private ActionResult ShowForm(string slug, AddLocationPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetBunchContext(slug);
            var model = new AddLocationPageModel(contextResult, postModel, errors);
            return View(model);
        }
    }
}