using System.Collections.Generic;
using Core.Classes;
using Core.Classes.Checkpoints;

namespace entities{

	public interface CashgameResultFactory{

		CashgameResult Create(Player player, List<Checkpoint> checkpoints);

	}

}