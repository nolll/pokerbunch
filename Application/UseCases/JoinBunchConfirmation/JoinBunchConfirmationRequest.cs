namespace Application.UseCases.JoinBunchConfirmation
{
    public class JoinBunchConfirmationRequest
    {
        public string Slug { get; private set; }

        public JoinBunchConfirmationRequest(string slug)
        {
            Slug = slug;
        }
    }
}