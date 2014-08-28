using Core.Entities;
using Tests.Common.FakeClasses;

namespace Tests.Common.Builders
{
    public class BunchBuilder
    {
        private int _id;
        private string _slug;
        private string _displayName;

        public BunchBuilder()
        {
            _id = 1;
            _slug = "a";
            _displayName = "b";
        }

        public Bunch Build()
        {
            return new BunchInTest(_id, _slug, _displayName);
        }

        public BunchBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public BunchBuilder WithSlug(string slug)
        {
            _slug = slug;
            return this;
        }

        public BunchBuilder WithDisplayName(string displayName)
        {
            _displayName = displayName;
            return this;
        }
    }
}