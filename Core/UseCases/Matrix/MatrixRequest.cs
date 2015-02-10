namespace Core.UseCases.Matrix
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

    public class EventMatrixRequest
    {
        public string Slug { get; private set; }
        public int EventId { get; private set; }

        public EventMatrixRequest(string slug, int eventId)
        {
            Slug = slug;
            EventId = eventId;
        }
    }
}