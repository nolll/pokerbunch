namespace Core.UseCases.Matrix
{
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