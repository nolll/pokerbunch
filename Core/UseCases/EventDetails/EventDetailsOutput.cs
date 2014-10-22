namespace Core.UseCases.EventDetails
{
    public class EventDetailsOutput
    {
        public string Name { get; private set; }

        public EventDetailsOutput(string name)
        {
            Name = name;
        }
    }
}