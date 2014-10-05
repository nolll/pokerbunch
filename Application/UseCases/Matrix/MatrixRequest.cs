namespace Application.UseCases.Matrix
{
    public class MatrixRequest
    {
        public string Slug { get; private set; }
        public int? Year { get; private set; }

        public MatrixRequest(string slug, int? year)
        {
            Slug = slug;
            Year = year;
        }
    }
}