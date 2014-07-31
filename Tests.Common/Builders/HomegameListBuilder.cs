using System.Collections.Generic;
using Core.Entities;

namespace Tests.Common.Builders
{
    public class HomegameListBuilder
    {
        private readonly IList<Homegame> _homegames; 

        public HomegameListBuilder()
        {
            _homegames = new List<Homegame>();
        }

        public IList<Homegame> Build()
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