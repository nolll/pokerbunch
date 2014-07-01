using Application.UseCases.CashgameContext;
using Tests.Common.FakeClasses;

namespace Tests.Common.FakeInteractors
{
    public class CashgameContextInteractorInTest : ICashgameContextInteractor
    {
        public CashgameContextResult Execute(CashgameContextRequest request)
        {
            return new CashgameContextResultInTest();
        }
    }
}