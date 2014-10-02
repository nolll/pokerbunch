namespace Application.UseCases.Matrix
{
    public class MatrixRequest
    {
        public string Slug { get; private set; }

        public MatrixRequest(string slug)
        {
            Slug = slug;
        }
    }
}