using System;
using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.Buyin
{
    public class BuyinRequest
    {
        public string Slug { get; private set; }
        public int PlayerId { get; private set; }
        [Range(1, int.MaxValue, ErrorMessage = "Amount needs to be positive")]
        public int BuyinAmount { get; private set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stack can't be negative")]
        public int StackAmount { get; private set; }
        public DateTime CurrentTime { get; private set; }

        public BuyinRequest(string slug, int playerId, int buyinAmount, int stackAmount, DateTime currentTime)
        {
            Slug = slug;
            PlayerId = playerId;
            BuyinAmount = buyinAmount;
            StackAmount = stackAmount;
            CurrentTime = currentTime;
        }
    }
}