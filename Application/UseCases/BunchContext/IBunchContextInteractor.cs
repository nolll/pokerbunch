namespace Application.UseCases.BunchContext
{
    public interface IBunchContextInteractor
    {
        BunchContextResult Execute(BunchContextRequest request);
    }
}