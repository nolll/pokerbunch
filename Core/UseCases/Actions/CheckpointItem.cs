using System;
using Core.Entities;
using Core.Urls;

namespace Core.UseCases.Actions
{
    public class CheckpointItem
    {
        public DateTime Time { get; private set; }
        public Url EditUrl { get; private set; }
        public string Type { get; private set; }
        public Money DisplayAmount { get; private set; }
        public bool CanEdit { get; private set; }

        public CheckpointItem(DateTime time, Url editUrl, string type, Money displayAmount, bool canEdit)
        {
            Time = time;
            EditUrl = editUrl;
            Type = type;
            DisplayAmount = displayAmount;
            CanEdit = canEdit;
        }
    }
}