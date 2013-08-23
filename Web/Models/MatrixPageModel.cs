using System.Collections.Generic;
using Core.Classes;
using Web.Models.Navigation;
using Web.Models.Url;

namespace Web.Models{
    public class MatrixPageModel : HomegamePageModel {

	    public CashgameNavigationModel CashgameNavModel { get; set; }
	    public TableModel TableModel { get; set; }
	    public bool CashgameIsRunning { get; set; }
	    public UrlModel CashgameUrl { get; set; }

	    public MatrixPageModel(User user, Homegame homegame, CashgameSuite suite, List<int> years, int? year, Cashgame runningGame) : base(user, homegame, runningGame)
	    {
	        TableModel = new TableModel(homegame, suite);
			CashgameNavModel = new CashgameNavigationModel(homegame, "matrix", years, year, runningGame);
	    }

	}

}