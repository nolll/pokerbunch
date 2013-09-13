using System.ComponentModel.DataAnnotations;
using Core.Classes;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Buyin{

    public class BuyinPageModel : HomegamePageModel {

        public bool StackFieldEnabled { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Amount needs to be positive")]
        public int BuyinAmount { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Stack can't be negative")]
        public int StackAmount { get; set; }

        public BuyinPageModel(User user, Homegame homegame, Player player, Cashgame runningGame)
            : base(user, homegame, runningGame)
        {
            StackFieldEnabled = runningGame.IsInGame(player);
        }

        public BuyinPageModel(User user, Homegame homegame, Player player, Cashgame runningGame, BuyinPageModel model)
            : this(user, homegame, player, runningGame)
        {
            BuyinAmount = model.BuyinAmount;
            StackAmount = model.StackAmount;
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