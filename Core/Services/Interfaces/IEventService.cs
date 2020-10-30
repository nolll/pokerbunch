using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface IEventService
    {
        IList<Event> ListByBunch(string bunchId);
    }
}