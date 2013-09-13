using Core.Classes;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Buyin{

    public class BuyinModel : HomegamePageModel {

        public UrlModel BuyinUrl { get; set; }
        public bool StackFieldEnabled { get; set; }
		public int BuyinAmount { get; set; }
        public int StackAmount { get; set; }

        public BuyinModel(User user, Homegame homegame, Player player, Cashgame runningGame, int? postedAmount = null)
            :base(user, homegame, runningGame)
        {
			BuyinUrl = new CashgameBuyinUrlModel(homegame, player);
			StackFieldEnabled = runningGame.IsInGame(player);
			BuyinAmount = postedAmount.HasValue ? postedAmount.Value : homegame.DefaultBuyin;
            StackAmount = 0;
        }

        public override string BrowserTitle
        {
            get
            {
                return "Buy In";
            }
        }

	}

}