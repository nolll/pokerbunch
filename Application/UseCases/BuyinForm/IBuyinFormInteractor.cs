namespace Application.UseCases.BuyinForm
{
    public interface IBuyinFormInteractor
    {
        BuyinFormResult Execute(BuyinFormRequest request);
    }
}