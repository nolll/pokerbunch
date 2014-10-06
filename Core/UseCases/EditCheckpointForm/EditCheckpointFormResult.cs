using System;
using Core.Urls;

namespace Core.UseCases.EditCheckpointForm
{
    public class EditCheckpointFormResult
    {
        public int Stack { get; private set; }
        public int Amount { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public Url DeleteUrl { get; private set; }
        public Url CancelUrl { get; private set; }
        public bool CanEditAmount { get; private set; }

        public EditCheckpointFormResult(int stack, int amount, DateTime timeStamp, Url deleteUrl, Url cancelUrl, bool canEditAmount)
        {
            TimeStamp = timeStamp;
            Stack = stack;
            Amount = amount;
            DeleteUrl = deleteUrl;
            CancelUrl = cancelUrl;
            CanEditAmount = canEditAmount;
        }
    }
}