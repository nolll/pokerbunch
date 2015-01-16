using Tests.Common.Builders;

namespace Tests.Common
{
    public class BuilderContainer
    {
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