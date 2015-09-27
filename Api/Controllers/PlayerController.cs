using System.Web.Http;
using Api.Auth;
using Api.Models;
using Core.UseCases;
using Web.Common.Routes;

namespace Api.Controllers
{
    public class PlayerController : BaseApiController
    {
        [Route(ApiRoutes.PlayerList)]
        [AcceptVerbs("GET")]
        [ApiAuthorize]
        public ApiPlayerList List(string slug)
        {
            var playerListResult = UseCase.PlayerList.Execute(new PlayerList.Request(CurrentUserName, slug));
            return new ApiPlayerList(playerListResult);
        }

        [Route(ApiRoutes.PlayerDetails)]
        [AcceptVerbs("GET")]
        [ApiAuthorize]
        public IHttpActionResult Details(int id)
        {
            var playerDetailsResult = UseCase.PlayerDetails.Execute(new PlayerDetails.Request(CurrentUserName, id));
            var bunchModel = new ApiPlayer(playerDetailsResult.DisplayName);
            return Ok(bunchModel);
        }
    }
}