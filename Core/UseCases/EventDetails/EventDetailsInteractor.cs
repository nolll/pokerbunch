using Core.Repositories;

namespace Core.UseCases.EventDetails
{
    public class EventDetailsInteractor
    {
        private readonly IEventRepository _eventRepository;

        public EventDetailsInteractor(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public EventDetailsOutput Execute(EventDetailsInput input)
        {
            var e = _eventRepository.GetById(input.EventId);

            return new EventDetailsOutput(e.Name);
        }
    }
}