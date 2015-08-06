using System.Web.Http;
using Api.Models;
using Core.UseCases;
using Core.UseCases.PlayerList;

namespace Api.Controllers
{
    public class PlayerController : BaseApiController
    {
        [Route("api/player/{slug}")]
        [AcceptVerbs("GET")]
        public ApiPlayerList List(string slug)
        {
            var playerListResult = UseCase.PlayerList.Execute(new PlayerListRequest(slug, "henriks"));
            return new ApiPlayerList(playerListResult);
        }

        [Route("api/player/{slug}/{playerId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Details(string slug, int playerId)
        {
            var playerDetailsResult = UseCase.PlayerDetails.Execute(new PlayerDetails.Request(slug, playerId, "henriks"));
            var bunchModel = new ApiPlayer(playerDetailsResult.DisplayName);
            return Ok(bunchModel);
        }
    }
}