using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
	public interface ICashgameService
	{
        IList<ListCashgame> PlayerList(string playerId);
    }
}