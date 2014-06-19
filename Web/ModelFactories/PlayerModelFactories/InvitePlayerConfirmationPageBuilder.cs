using Application.UseCases.BunchContext;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class InvitePlayerConfirmationPageBuilder : IInvitePlayerConfirmationPageBuilder
    {
        private readonly IBunchContextInteractor _bunchContextInteractor;

        public InvitePlayerConfirmationPageBuilder(
            IBunchContextInteractor bunchContextInteractor)
        {
            _bunchContextInteractor = bunchContextInteractor;
        }

        public InvitePlayerConfirmationPageModel Build(string slug)
        {
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest { Slug = slug });

            return new InvitePlayerConfirmationPageModel(contextResult);
        }
    }
}