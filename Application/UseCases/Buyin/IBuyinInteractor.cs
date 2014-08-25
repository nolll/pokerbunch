namespace Application.UseCases.Buyin
{
    public interface IBuyinInteractor
    {
        BuyinResult Execute(BuyinRequest request);
    }
}