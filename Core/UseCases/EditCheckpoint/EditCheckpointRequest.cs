﻿using System;

namespace Core.UseCases.EditCheckpoint
{
    public class EditCheckpointRequest
    {
        public string Slug { get; private set; }
        public string DateStr { get; private set; }
        public int PlayerId { get; private set; }
        public int CheckpointId { get; private set; }
        public DateTime Timestamp { get; private set; }
        public int Stack { get; private set; }
        public int Amount { get; private set; }

        public EditCheckpointRequest(string slug, string dateStr, int playerId, int checkpointId, DateTime timestamp, int stack, int amount)
        {
            Slug = slug;
            DateStr = dateStr;
            PlayerId = playerId;
            CheckpointId = checkpointId;
            Timestamp = timestamp;
            Stack = stack;
            Amount = amount;
        }
    }
}