using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;

namespace Core.Factories.Interfaces
{
	public interface ICashgameResultFactory
    {
		CashgameResult Create(int playerId, List<Checkpoint> checkpoints);
	}
}