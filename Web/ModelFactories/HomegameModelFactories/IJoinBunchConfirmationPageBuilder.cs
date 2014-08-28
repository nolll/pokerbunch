using Web.Models.HomegameModels.Join;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IJoinBunchConfirmationPageBuilder
    {
        JoinBunchConfirmationPageModel Build(string slug);
    }
}