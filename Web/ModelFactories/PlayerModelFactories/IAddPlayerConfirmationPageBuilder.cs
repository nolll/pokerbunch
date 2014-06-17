using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IAddPlayerConfirmationPageBuilder
    {
        AddPlayerConfirmationPageModel Build(string slug);
    }
}