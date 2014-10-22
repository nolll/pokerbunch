using System.Collections.Generic;
using Infrastructure.SqlServer.Classes;

namespace Infrastructure.SqlServer.Interfaces {

	public interface IBunchStorage{

        IList<RawBunch> GetBunchesByUserId(int userId);
        int GetBunchRole(int bunchId, int userId);
        RawBunch GetBunchByName(string name);
        RawBunch AddBunch(RawBunch bunch);
        bool UpdateBunch(RawBunch bunch);
		bool DeleteBunch(string slug);
        IList<int> GetAllIds();
	    IList<RawBunch> GetBunches(IList<int> ids);
	    int? GetIdBySlug(string slug);
	    RawBunch GetById(int id);
	}

}