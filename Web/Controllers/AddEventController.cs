using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.EventModels.Add;

namespace Web.Controllers
{
    public class AddEventController : BaseController
    {
        [Authorize]
        [Route(AddEventUrl.Route)]
        public ActionResult Add(string bunchId)
        {
            return ShowForm(bunchId);
        }

        [HttpPost]
        [Authorize]
        [Route(AddEventUrl.Route)]
        public ActionResult Add_Post(string bunchId, AddEventPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new AddEvent.Request(bunchId, postModel.Name);
                var result = UseCase.AddEvent.Execute(request);
                return Redirect(new AddEventConfirmationUrl(result.Slug).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }

            return ShowForm(bunchId, postModel, errors);
        }

        [Route(AddEventConfirmationUrl.Route)]
        public ActionResult Created(string bunchId)
        {
            var contextResult = GetBunchContext(bunchId);
            var model = new AddEventConfirmationPageModel(contextResult);
            return View(model);
        }

        private ActionResult ShowForm(string bunchId, AddEventPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetBunchContext(bunchId);
            var model = new AddEventPageModel(contextResult, postModel, errors);
            return View(model);
        }
    }
}