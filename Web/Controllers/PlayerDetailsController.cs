using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Details;

namespace Web.Controllers
{
    public class PlayerDetailsController : BunchController
    {
        private readonly PlayerDetails _playerDetails;
        private readonly PlayerFacts _playerFacts;
        private readonly PlayerBadges _playerBadges;

        public PlayerDetailsController(AppSettings appSettings, CoreContext coreContext, BunchContext bunchContext, PlayerDetails playerDetails, PlayerFacts playerFacts, PlayerBadges playerBadges) 
            : base(appSettings, coreContext, bunchContext)
        {
            _playerDetails = playerDetails;
            _playerFacts = playerFacts;
            _playerBadges = playerBadges;
        }

        [Authorize]
        [Route(PlayerDetailsUrl.Route)]
        public ActionResult Details(string playerId)
        {
            var detailsResult = _playerDetails.Execute(new PlayerDetails.Request(playerId));
            var contextResult = GetBunchContext(detailsResult.Slug);
            var factsResult = _playerFacts.Execute(new PlayerFacts.Request(playerId));
            var badgesResult = _playerBadges.Execute(new PlayerBadges.Request(playerId));
            var model = new PlayerDetailsPageModel(AppSettings, contextResult, detailsResult, factsResult, badgesResult);
            return View(model);
        }
    }
}