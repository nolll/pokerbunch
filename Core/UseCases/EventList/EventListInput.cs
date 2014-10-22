namespace Core.UseCases.EventList
{
    public class EventListInput
    {
        public string Slug { get; private set; }

        public EventListInput(string slug)
        {
            Slug = slug;
        }
    }
}