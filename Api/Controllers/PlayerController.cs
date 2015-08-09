using System.Web.Http;
using Api.Models;
using Core.UseCases;
using Core.UseCases.PlayerList;

namespace Api.Controllers
{
    public class PlayerController : BaseApiController
    {
        [Route("players/{slug}")]
        [AcceptVerbs("GET")]
        public ApiPlayerList List(string slug)
        {
            var playerListResult = UseCase.PlayerList.Execute(new PlayerListRequest(slug, "henriks"));
            return new ApiPlayerList(playerListResult);
        }

        [Route("player/{playerId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Details(int playerId)
        {
            var playerDetailsResult = UseCase.PlayerDetails.Execute(new PlayerDetails.Request(playerId, "henriks"));
            var bunchModel = new ApiPlayer(playerDetailsResult.DisplayName);
            return Ok(bunchModel);
        }
    }
}