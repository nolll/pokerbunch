namespace Application.UseCases.EditBunchForm
{
    public class EditBunchFormRequest
    {
        public string Slug { get; private set; }

        public EditBunchFormRequest(string slug)
        {
            Slug = slug;
        }
    }
}