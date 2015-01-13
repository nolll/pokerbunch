using System.Collections.Generic;
using Infrastructure.Storage.Classes;

namespace Infrastructure.Storage.Interfaces
{
	public interface IBunchStorage
    {
        IList<int> GetBunchIdsByUserId(int userId);
        int GetBunchRole(int bunchId, int userId);
        RawBunch GetBunchByName(string name);
        int AddBunch(RawBunch bunch);
        bool UpdateBunch(RawBunch bunch);
		bool DeleteBunch(string slug);
        IList<int> GetAllIds();
	    IList<RawBunch> GetBunches(IList<int> ids);
	    int? GetIdBySlug(string slug);
	    RawBunch GetById(int id);
	}
}