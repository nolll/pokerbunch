using Application.Services;
using Core.UseCases;
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

        public UserItemModel Create(UserItem userItem)
        {
            return new UserItemModel
                {
                    Name = userItem.Name,
                    UrlModel = _urlProvider.GetUserDetailsUrl(userItem.Identifier)
                };
        }
    }
}