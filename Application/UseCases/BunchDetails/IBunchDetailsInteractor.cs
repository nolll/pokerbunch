namespace Application.UseCases.BunchDetails
{
    public interface IBunchDetailsInteractor
    {
        BunchDetailsResult Execute(BunchDetailsRequest request);
    }
}