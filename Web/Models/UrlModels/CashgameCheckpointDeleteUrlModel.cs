using System.Web;
using Core.Classes;
using Core.Classes.Checkpoints;
using Web.Formatters;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class CashgameCheckpointDeleteUrlModel : UrlModel{

		public CashgameCheckpointDeleteUrlModel(Homegame homegame, Cashgame cashgame, Player player, Checkpoint checkpoint)
			: base(GetUrl(homegame, cashgame, player, checkpoint)) {}

        private static string GetUrl(Homegame homegame, Cashgame cashgame, Player player, Checkpoint checkpoint)
        {
            var isoDate = cashgame.StartTime.HasValue ? UrlFormatter.FormatIsoDate(cashgame.StartTime.Value) : string.Empty;
			var encodedPlayerName = HttpUtility.UrlPathEncode(player.DisplayName);
			return string.Format(RouteFormats.CashgameCheckpointDelete, homegame.Slug, isoDate, encodedPlayerName, checkpoint.Id);
        }

	}

}