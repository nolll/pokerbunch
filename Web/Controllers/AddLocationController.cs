using System.Collections.Generic;
using Core.Exceptions;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.LocationModels.Add;

namespace Web.Controllers
{
    public class AddLocationController : BunchController
    {
        private readonly AddLocation _addLocation;

        public AddLocationController(AppSettings appSettings, CoreContext coreContext, BunchContext bunchContext, AddLocation addLocation) 
            : base(appSettings, coreContext, bunchContext)
        {
            _addLocation = addLocation;
        }

        [Authorize]
        [Route(AddLocationUrl.Route)]
        public ActionResult Add(string bunchId)
        {
            return ShowForm(bunchId);
        }

        [HttpPost]
        [Authorize]
        [Route(AddLocationUrl.Route)]
        public ActionResult Add_Post(string bunchId, AddLocationPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new AddLocation.Request(bunchId, postModel.Name);
                var result = _addLocation.Execute(request);
                return Redirect(new AddLocationConfirmationUrl(result.Slug).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }

            return ShowForm(bunchId, postModel, errors);
        }

        [Route(AddLocationConfirmationUrl.Route)]
        public ActionResult Created(string bunchId)
        {
            var contextResult = GetBunchContext(bunchId);
            var model = new AddLocationConfirmationPageModel(AppSettings, contextResult);
            return View(model);
        }

        private ActionResult ShowForm(string bunchId, AddLocationPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetBunchContext(bunchId);
            var model = new AddLocationPageModel(AppSettings, contextResult, postModel, errors);
            return View(model);
        }
    }
}