using Application.UseCases.BunchContext;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class InvitePlayerConfirmationPageBuilder : IInvitePlayerConfirmationPageBuilder
    {
        private readonly IBunchContextInteractor _contextInteractor;

        public InvitePlayerConfirmationPageBuilder(
            IBunchContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public InvitePlayerConfirmationPageModel Build(string slug)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest { Slug = slug });

            return new InvitePlayerConfirmationPageModel(contextResult);
        }
    }
}