using System.Collections.Generic;
using Core.Classes;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Matrix{
    public class CashgameMatrixPageModel : IPageModel {

        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
	    public CashgameNavigationModel CashgameNavModel { get; set; }
	    public CashgameMatrixTableModel TableModel { get; set; }
	    public bool CashgameIsRunning { get; set; }
	    public UrlModel CashgameUrl { get; set; }

	    public CashgameMatrixPageModel(User user, Homegame homegame, CashgameSuite suite, List<int> years, int? year, Cashgame runningGame)
	    {
            BrowserTitle = "Cashgame Matrix";
            PageProperties = new PageProperties(user, homegame, runningGame);
	        TableModel = new CashgameMatrixTableModel(homegame, suite);
			CashgameNavModel = new CashgameNavigationModel(homegame, "matrix", years, year, runningGame);
	    }

	}

}