using Tests.Common.Builders;

namespace Tests.Common
{
    public class BuilderContainer
    {
        public BunchBuilder Bunch
        {
            get { return new BunchBuilder(); }
        }

        public BunchListBuilder BunchList
        {
            get { return new BunchListBuilder(); }
        }

        public UserBuilder User
        {
            get { return new UserBuilder(); }
        }

        public RawUserBuilder RawUser
        {
            get { return new RawUserBuilder(); }
        }

        public CashgameBuilder Cashgame
        {
            get { return new CashgameBuilder(); }
        }

        public CheckpointBuilder Checkpoint
        {
            get { return new CheckpointBuilder(); }
        }

        public DateTimeBuilder DateTime
        {
            get { return new DateTimeBuilder(); }
        }
    }
}