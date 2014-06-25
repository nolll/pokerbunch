using Application.UseCases.AppContext;
using Application.UseCases.UserDetails;
using Web.Models.UserModels;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserDetailsPageBuilder : IUserDetailsPageBuilder
    {
        private readonly IAppContextInteractor _contextInteractor;
        private readonly IUserDetailsInteractor _userDetailsInteractor;

        public UserDetailsPageBuilder(
            IAppContextInteractor contextInteractor,
            IUserDetailsInteractor userDetailsInteractor)
        {
            _contextInteractor = contextInteractor;
            _userDetailsInteractor = userDetailsInteractor;
        }

        public UserDetailsPageModel Build(string userName)
        {
            var contextResult = _contextInteractor.Execute();
            var userDetailsResult = _userDetailsInteractor.Execute(new UserDetailsRequest(userName));

            return new UserDetailsPageModel(contextResult, userDetailsResult);
        }
    }
}