using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;
using Web.ModelFactories.SharingModelFactories;

namespace Web.Controllers{

	public class SharingController : Controller {
	    private readonly IUserContext _userContext;
	    private readonly ISharingStorage _sharingStorage;
	    private readonly ISharingIndexPageModelFactory _sharingIndexPageModelFactory;

	    public SharingController(
            IUserContext userContext,
			ISharingStorage sharingStorage,
            ISharingIndexPageModelFactory sharingIndexPageModelFactory)
	    {
	        _userContext = userContext;
	        _sharingStorage = sharingStorage;
	        _sharingIndexPageModelFactory = sharingIndexPageModelFactory;
	    }

	    public ActionResult Index(){
			_userContext.RequireUser();
			var user = _userContext.GetUser();
			var isSharing = _sharingStorage.IsSharing(user, SocialServiceProvider.Twitter);
			var model = _sharingIndexPageModelFactory.Create(user, isSharing);
			return View("Index", model);
		}

	}

}