namespace Core.UseCases.EventDetails
{
    public class EventDetailsInput
    {
        public int EventId { get; private set; }

        public EventDetailsInput(int eventId)
        {
            EventId = eventId;
        }
    }
}