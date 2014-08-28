using Core.Entities;
using Tests.Common.FakeClasses;

namespace Tests.Common.Builders
{
    public class HomegameBuilder
    {
        private int _id;
        private string _slug;
        private string _displayName;

        public HomegameBuilder()
        {
            _id = 1;
            _slug = "a";
            _displayName = "b";
        }

        public Bunch Build()
        {
            return new BunchInTest(_id, _slug, _displayName);
        }

        public HomegameBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public HomegameBuilder WithSlug(string slug)
        {
            _slug = slug;
            return this;
        }

        public HomegameBuilder WithDisplayName(string displayName)
        {
            _displayName = displayName;
            return this;
        }
    }
}