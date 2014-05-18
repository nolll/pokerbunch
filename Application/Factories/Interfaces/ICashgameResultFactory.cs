using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;

namespace Application.Factories
{
	public interface ICashgameResultFactory
    {
		CashgameResult Create(int playerId, List<Checkpoint> checkpoints);
	}
}