using System.Collections.Generic;
using Core.Classes;
using Core.Classes.Checkpoints;

namespace Application.Factories
{
	public interface ICashgameResultFactory
    {
		CashgameResult Create(int playerId, List<Checkpoint> checkpoints);
	}
}