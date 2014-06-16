using Core.Entities;
using Web.Models.HomegameModels.Join;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IJoinHomegameConfirmationPageBuilder
    {
        JoinHomegameConfirmationPageModel Build(string slug);
    }
}