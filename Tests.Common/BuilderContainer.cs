using Tests.Common.Builders;

namespace Tests.Common
{
    public class BuilderContainer
    {
        public DateTimeBuilder DateTime
        {
            get { return new DateTimeBuilder(); }
        }
    }
}