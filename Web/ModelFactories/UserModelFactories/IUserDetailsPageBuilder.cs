using Web.Models.UserModels;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IUserDetailsPageBuilder
    {
        UserDetailsPageModel Build(string userName);
    }
}