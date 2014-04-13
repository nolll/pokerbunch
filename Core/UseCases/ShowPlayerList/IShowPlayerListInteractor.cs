namespace Core.UseCases.ShowPlayerList
{
    public interface IShowPlayerListInteractor
    {
        ShowPlayerListResult Execute(string slug);
    }
}