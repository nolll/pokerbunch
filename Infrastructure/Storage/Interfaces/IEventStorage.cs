using System.Collections.Generic;
using Infrastructure.Storage.Classes;

namespace Infrastructure.Storage.Interfaces
{
	public interface IEventStorage
	{
	    RawEvent GetById(int id);
		IList<RawEvent> GetEventList(IList<int> ids);
        IList<int> GetEventIdList(int bunchId);
    }
}