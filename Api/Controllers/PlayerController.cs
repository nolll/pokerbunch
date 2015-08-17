using System.Web.Http;
using Api.Models;
using Core.UseCases;

namespace Api.Controllers
{
    public class PlayerController : BaseApiController
    {
        [Route("players/{slug}")]
        [AcceptVerbs("GET")]
        public ApiPlayerList List(string slug)
        {
            var playerListResult = UseCase.PlayerList.Execute(new PlayerList.Request("henriks", slug));
            return new ApiPlayerList(playerListResult);
        }

        [Route("player/{playerId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Details(int playerId)
        {
            var playerDetailsResult = UseCase.PlayerDetails.Execute(new PlayerDetails.Request("henriks", playerId));
            var bunchModel = new ApiPlayer(playerDetailsResult.DisplayName);
            return Ok(bunchModel);
        }
    }
}