using System.Web.Mvc;
using Core.Urls;
using Web.Commands.HomegameCommands;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Add;

namespace Web.Controllers
{
    public class AddBunchController : PokerBunchController
    {
        private readonly IBunchCommandProvider _bunchCommandProvider;

        public AddBunchController(IBunchCommandProvider bunchCommandProvider)
        {
            _bunchCommandProvider = bunchCommandProvider;
        }

        [Authorize]
        [Route("-/homegame/add")]
        public ActionResult Add()
        {
            return GetForm();
        }

        [HttpPost]
        [Authorize]
        [Route("-/homegame/add")]
        public ActionResult Add_Post(AddBunchPostModel postModel)
        {
            var command = _bunchCommandProvider.GetAddCommand(postModel);
            if (command.Execute())
            {
                return Redirect(new AddBunchConfirmationUrl().Relative);
            }
            AddModelErrors(command.Errors);
            return GetForm(postModel);
        }

        [Route("-/homegame/created")]
        public ActionResult Created()
        {
            var contextResult = UseCase.AppContext();
            var model = new AddBunchConfirmationPageModel(contextResult);
            return View("AddBunch/AddHomegameConfirmation", model);
        }

        private ActionResult GetForm(AddBunchPostModel postModel = null)
        {
            var contextResult = UseCase.AppContext();
            var bunchFormResult = UseCase.AddBunchForm();
            var model = new AddBunchPageModel(contextResult, bunchFormResult, postModel);
            return View("AddBunch/AddHomegame", model);
        }
    }
}