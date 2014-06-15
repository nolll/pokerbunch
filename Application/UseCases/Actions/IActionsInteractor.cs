namespace Application.UseCases.Actions
{
    public interface IActionsInteractor
    {
        ActionsResult Execute(ActionsRequest request);
    }
}