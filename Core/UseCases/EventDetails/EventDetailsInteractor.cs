using Core.Repositories;

namespace Core.UseCases.EventDetails
{
    public class EventDetailsInteractor
    {
        public static EventDetailsOutput Execute(IEventRepository eventRepository, EventDetailsInput input)
        {
            var e = eventRepository.GetById(input.EventId);

            return new EventDetailsOutput(e.Name);
        }
    }
}