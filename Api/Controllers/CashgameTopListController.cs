using System.Web.Http;
using Api.Models;
using Core.UseCases;

namespace Api.Controllers
{
    public class CashgameTopListController : BaseApiController
    {
        [Route("cashgame/toplist/{slug}/{year?}")]
        [AcceptVerbs("GET")]
        public ApiCashgameTopList Index(string slug, int? year = null)
        {
            var topListResult = UseCase.TopList.Execute(new TopList.Request(slug, TopList.SortOrder.Winnings, year));
            return new ApiCashgameTopList(topListResult);
        }
    }
}