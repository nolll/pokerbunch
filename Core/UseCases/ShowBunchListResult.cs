﻿using System.Collections.Generic;

namespace Core.UseCases
{
    public class ShowBunchListResult
    {
        public IList<BunchListItem> Bunches { get; set; }
    }
}