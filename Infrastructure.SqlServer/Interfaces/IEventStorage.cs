using System.Collections.Generic;
using Infrastructure.SqlServer.Classes;

namespace Infrastructure.SqlServer.Interfaces
{
	public interface IEventStorage
	{
	    RawEvent GetById(int id);
		IList<RawEvent> GetEventList(IList<int> ids);
        IList<int> GetEventIdList();
    }
}