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
            var model = Build(slug);
            if (postModel != null)
            {
                model.Email = postModel.Email;
            }
            return model;
        }

        private InvitePlayerPageModel Build(string slug)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest{Slug = slug});

            return new InvitePlayerPageModel
                {
                    BrowserTitle = "Invite Player",
                    PageProperties = new PageProperties(contextResult)
                };
        }
    }
}