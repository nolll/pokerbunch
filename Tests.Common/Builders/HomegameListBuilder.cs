using System.Collections.Generic;
using Core.Entities;

namespace Tests.Common.Builders
{
    public class HomegameListBuilder
    {
        private readonly IList<Bunch> _homegames; 

        public HomegameListBuilder()
        {
            _homegames = new List<Bunch>();
        }

        public IList<Bunch> Build()
        {
            return _homegames;
        }

        public HomegameListBuilder WithOneItem()
        {
            var homegameBuilder = new HomegameBuilder();
            _homegames.Add(homegameBuilder.Build());
            return this;
        }
    }
}