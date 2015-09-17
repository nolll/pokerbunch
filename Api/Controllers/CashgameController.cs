using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Api.Annotations;
using Api.Models;
using Core.Exceptions;
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

        [Route("cashgame/running/{slug}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Running(string slug)
        {
            try
            {
                var runningResult = UseCase.RunningCashgame.Execute(new RunningCashgame.Request(CurrentUserName, slug));
                return Ok(new ApiRunning(runningResult));
            }
            catch (CashgameNotRunningException e)
            {
                return new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.NoContent));
            }
        }

        [Route("cashgame/buyin/{slug}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Buyin(string slug, [FromBody] BuyinObject buyin)
        {
            try
            {
                UseCase.Buyin.Execute(new Buyin.Request(CurrentUserName, slug, buyin.playerid, buyin.amount, buyin.stack, DateTime.UtcNow));
                return Ok();
            }
            catch (CashgameNotRunningException e)
            {
                return InternalServerError();
            }
        }

        public class BuyinObject
        {
            // ReSharper disable once InconsistentNaming
            public int playerid { get; [UsedImplicitly] set; }
            // ReSharper disable once InconsistentNaming
            public int amount { get; [UsedImplicitly] set; }
            // ReSharper disable once InconsistentNaming
            public int stack { get; [UsedImplicitly] set; }
        }

        [Route("cashgame/cashout/{slug}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Buyin(string slug, [FromBody] CashoutObject buyin)
        {
            try
            {
                UseCase.Cashout.Execute(new Cashout.Request(CurrentUserName, slug, buyin.playerid, buyin.stack, DateTime.UtcNow));
                return Ok();
            }
            catch (CashgameNotRunningException e)
            {
                return InternalServerError();
            }
        }

        public class CashoutObject
        {
            // ReSharper disable once InconsistentNaming
            public int playerid { get; [UsedImplicitly] set; }
            // ReSharper disable once InconsistentNaming
            public int stack { get; [UsedImplicitly] set; }
        }

        [Route("cashgame/report/{slug}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Report(string slug, [FromBody] CashoutObject buyin)
        {
            try
            {
                UseCase.Report.Execute(new Report.Request(CurrentUserName, slug, buyin.playerid, buyin.stack, DateTime.UtcNow));
                return Ok();
            }
            catch (CashgameNotRunningException e)
            {
                return InternalServerError();
            }
        }

        public class ReportObject
        {
            // ReSharper disable once InconsistentNaming
            public int playerid { get; [UsedImplicitly] set; }
            // ReSharper disable once InconsistentNaming
            public int stack { get; [UsedImplicitly] set; }
        }
    }
}