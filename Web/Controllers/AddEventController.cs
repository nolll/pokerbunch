using System.Collections.Generic;
using Core.Exceptions;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.EventModels.Add;

namespace Web.Controllers
{
    public class AddEventController : BunchController
    {
        private readonly AddEvent _addEvent;

        public AddEventController(AppSettings appSettings, CoreContext coreContext, BunchContext bunchContext, AddEvent addEvent)
            : base(appSettings, coreContext, bunchContext)
        {
            _addEvent = addEvent;
        }

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
                var result = _addEvent.Execute(request);
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
            var model = new AddEventConfirmationPageModel(AppSettings, contextResult);
            return View(model);
        }

        private ActionResult ShowForm(string bunchId, AddEventPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetBunchContext(bunchId);
            var model = new AddEventPageModel(AppSettings, contextResult, postModel, errors);
            return View(model);
        }
    }
}