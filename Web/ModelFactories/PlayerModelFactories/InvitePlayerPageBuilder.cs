using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class InvitePlayerPageBuilder : IInvitePlayerPageBuilder
    {
        private readonly IBunchContextInteractor _bunchContextInteractor;

        public InvitePlayerPageBuilder(
            IBunchContextInteractor bunchContextInteractor)
        {
            _bunchContextInteractor = bunchContextInteractor;
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
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest{Slug = slug});

            return new InvitePlayerPageModel
                {
                    BrowserTitle = "Invite Player",
                    PageProperties = new PageProperties(contextResult)
                };
        }
    }
}