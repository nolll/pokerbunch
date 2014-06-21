using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class InvitePlayerPageBuilder : IInvitePlayerPageBuilder
    {
        private readonly IBunchContextInteractor _contextInteractor;

        public InvitePlayerPageBuilder(
            IBunchContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public InvitePlayerPageModel Build(string slug, InvitePlayerPostModel postModel)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));

            return new InvitePlayerPageModel(contextResult, postModel);
        }
    }
}