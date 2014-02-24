using System;
using Core.Classes.Checkpoints;

namespace Application.Factories
{
	public interface ICheckpointFactory
	{
        Checkpoint Create(DateTime timestamp, CheckpointType type, int stack, int amount = default(int), int id = default(int));
	}
}