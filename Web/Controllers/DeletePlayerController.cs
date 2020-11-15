using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeletePlayerController : BaseController
    {
        private readonly DeletePlayer _deletePlayer;

        public DeletePlayerController(AppSettings appSettings, DeletePlayer deletePlayer) 
            : base(appSettings)
        {
            _deletePlayer = deletePlayer;
        }

        [Authorize]
        [Route(DeletePlayerUrl.Route)]
        public ActionResult Delete(string playerId)
        {
            var request = new DeletePlayer.Request(playerId);
            var result = _deletePlayer.Execute(request);
            var returnUrl = CreateReturnUrl(result);
            return Redirect(returnUrl.Relative);
        }

        private static SiteUrl CreateReturnUrl(DeletePlayer.Result result)
        {
            if (result.Deleted)
                return new PlayerIndexUrl(result.Slug);
            return new PlayerDetailsUrl(result.Slug, result.PlayerId);
        }
    }
}