using System.Collections.Generic;
using Core.Exceptions;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Add;

namespace Web.Controllers
{
    public class AddBunchController : CoreController
    {
        private readonly AddBunch _addBunch;
        private readonly AddBunchForm _addBunchForm;

        public AddBunchController(AppSettings appSettings, CoreContext coreContext, AddBunch addBunch, AddBunchForm addBunchForm)
            : base(appSettings, coreContext)
        {
            _addBunch = addBunch;
            _addBunchForm = addBunchForm;
        }

        [Authorize]
        [Route(AddBunchUrl.Route)]
        public ActionResult Add()
        {
            return ShowForm();
        }

        [HttpPost]
        [Authorize]
        [Route(AddBunchUrl.Route)]
        public ActionResult Add_Post(AddBunchPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new AddBunch.Request(postModel.DisplayName, postModel.Description, postModel.CurrencySymbol, postModel.CurrencyLayout, postModel.TimeZone);
                _addBunch.Execute(request);
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

        [Route(AddBunchConfirmationUrl.Route)]
        public ActionResult Created()
        {
            var contextResult = GetAppContext();
            var model = new AddBunchConfirmationPageModel(AppSettings, contextResult);
            return View(model);
        }

        private ActionResult ShowForm(AddBunchPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetAppContext();
            var bunchFormResult = _addBunchForm.Execute();
            var model = new AddBunchPageModel(AppSettings, contextResult, bunchFormResult, postModel, errors);
            return View(model);
        }
    }
}