using System.Web.Mvc;
using Core.Repositories;
using Web.Models.Url;

namespace Web.Controllers{

	public class CashgameController : Controller {
	    private readonly IHomegameRepository _homegameRepository;
	    private readonly IUserContext _userContext;
	    private readonly ICashgameRepository _cashgameRepository;

	    public CashgameController(IHomegameRepository homegameRepository, IUserContext userContext, ICashgameRepository cashgameRepository)
	    {
	        _homegameRepository = homegameRepository;
	        _userContext = userContext;
	        _cashgameRepository = cashgameRepository;
	    }

	    public ActionResult Index(string game){
			var homegame = _homegameRepository.GetByName(game);
			_userContext.RequirePlayer(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			if(years.Count > 0){
				var year = years[0];
				return new RedirectResult(new CashgameMatrixUrlModel(homegame, year).Url);
			}
			return new RedirectResult(new CashgameAddUrlModel(homegame).Url);
		}

	}

}