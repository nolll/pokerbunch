using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Buyin
{
    public class BuyinRequest
    {
        public string Slug { get; private set; }
        public int PlayerId { get; private set; }
        [Range(1, int.MaxValue, ErrorMessage = "Amount needs to be positive")]
        public int BuyinAmount { get; private set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stack can't be negative")]
        public int StackAmount { get; private set; }

        public BuyinRequest(string slug, int playerId, int buyinAmount, int stackAmount)
        {
            Slug = slug;
            PlayerId = playerId;
            BuyinAmount = buyinAmount;
            StackAmount = stackAmount;
        }
    }
}