namespace Application.UseCases.JoinBunchConfirmation
{
    public interface IJoinBunchConfirmationInteractor
    {
        JoinBunchConfirmationResult Execute(JoinBunchConfirmationRequest request);
    }
}