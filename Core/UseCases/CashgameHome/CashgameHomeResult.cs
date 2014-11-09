using Core.Urls;

namespace Core.UseCases.CashgameHome
{
    public class CashgameHomeResult
    {
        public Url StartUrl { get; private set; }

        public CashgameHomeResult(Url startUrl)
        {
            StartUrl = startUrl;
        }
    }
}