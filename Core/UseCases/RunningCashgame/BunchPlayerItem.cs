namespace Core.UseCases.RunningCashgame
{
    public class BunchPlayerItem
    {
        public int PlayerId { get; private set; }
        public string Name { get; private set; }

        public BunchPlayerItem(int playerId, string name)
        {
            PlayerId = playerId;
            Name = name;
        }
    }
}