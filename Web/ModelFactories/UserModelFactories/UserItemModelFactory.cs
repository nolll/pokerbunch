using Core.Classes;
using Core.Services;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserItemModelFactory : IUserItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public UserItemModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public UserItemModel Create(User user)
        {
            return new UserItemModel
                {
                    Name = user.DisplayName,
                    UrlModel = _urlProvider.GetUserDetailsUrl(user)
                };
        }
    }
}