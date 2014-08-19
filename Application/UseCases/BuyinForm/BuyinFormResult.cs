namespace Application.UseCases.BuyinForm
{
    public class BuyinFormResult
    {
        public int BuyinAmount { get; private set; }

        public BuyinFormResult(int buyinAmount)
        {
            BuyinAmount = buyinAmount;
        }
    }
}