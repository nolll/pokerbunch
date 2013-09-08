using System.Collections.Generic;
using Core.Classes;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Matrix{
    public class CashgameMatrixPageModel : HomegamePageModel {

	    public CashgameNavigationModel CashgameNavModel { get; set; }
	    public CashgameMatrixTableModel TableModel { get; set; }
	    public bool CashgameIsRunning { get; set; }
	    public UrlModel CashgameUrl { get; set; }

	    public CashgameMatrixPageModel(User user, Homegame homegame, CashgameSuite suite, List<int> years, int? year, Cashgame runningGame) : base(user, homegame, runningGame)
	    {
	        TableModel = new CashgameMatrixTableModel(homegame, suite);
			CashgameNavModel = new CashgameNavigationModel(homegame, "matrix", years, year, runningGame);
	    }

        public override string BrowserTitle
        {
            get
            {
                return "Cashgame Matrix";
            }
        }

	}

}