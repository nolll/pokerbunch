using Core.Urls;

namespace Core.UseCases.Cashout
{
    public class CashoutResult
    {
        public Url ReturnUrl { get; private set; }

        public CashoutResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}