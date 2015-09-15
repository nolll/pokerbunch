using System.Web.Http;
using Api.Models;
using Core.UseCases;

namespace Api.Controllers
{
    public class CashgameController : BaseApiController
    {
        [Route("cashgame/toplist/{slug}/{year?}")]
        [AcceptVerbs("GET")]
        public ApiCashgameTopList TopListAction(string slug, int? year = null)
        {
            var topListResult = UseCase.TopList.Execute(new TopList.Request(CurrentUserName, slug, TopList.SortOrder.Winnings, year));
            return new ApiCashgameTopList(topListResult);
        }

        //[Route("cashgame/running/{slug}")]
        //[AcceptVerbs("GET")]
        //public ApiCashgameTopList Running(string slug)
        //{
        //    var runningResult = UseCase.RunningCashgame.Execute(new RunningCashgame.Request(CurrentUserName, slug));
        //    return new ApiCashgameTopList(runningResult);
        //}
    }
}