namespace Application.UseCases.JoinBunchForm
{
    public interface IJoinBunchFormInteractor
    {
        JoinBunchFormResult Execute(JoinBunchFormRequest request);
    }
}