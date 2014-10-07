using System.Collections.Generic;
using Core.Entities;

namespace Tests.Common.Builders
{
    public class BunchListBuilder
    {
        private readonly IList<Bunch> _bunches; 

        public BunchListBuilder()
        {
            _bunches = new List<Bunch>();
        }

        public IList<Bunch> Build()
        {
            return _bunches;
        }

        public BunchListBuilder WithOneItem()
        {
            var bunchBuilder = new BunchBuilder();
            var bunch = bunchBuilder.WithSlug("a").WithDisplayName("b").Build();
            _bunches.Add(bunch);
            return this;
        }
    }
}