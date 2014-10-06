namespace Core.UseCases.BuyinForm
{
    public class BuyinFormResult
    {
        public int BuyinAmount { get; private set; }
        public bool CanEnterStack { get; private set; }

        public BuyinFormResult(int buyinAmount, bool canEnterStack)
        {
            BuyinAmount = buyinAmount;
            CanEnterStack = canEnterStack;
        }
    }
}