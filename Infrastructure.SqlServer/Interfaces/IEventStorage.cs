using System.Collections.Generic;
using Infrastructure.Storage;

namespace Infrastructure.SqlServer.Interfaces
{
	public interface IEventStorage
	{
	    RawEvent GetById(int id);
		IList<RawEvent> GetEventList(IList<int> ids);
        IList<int> GetEventIdList();
    }
}