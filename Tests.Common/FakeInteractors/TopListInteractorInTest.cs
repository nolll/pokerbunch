using Application.UseCases.CashgameTopList;
using Tests.Common.FakeClasses;

namespace Tests.Common.FakeInteractors
{
    public class TopListInteractorInTest : ITopListInteractor
    {
        public TopListResult Execute(TopListRequest request)
        {
            return new TopListResultInTest();
        }
    }
}