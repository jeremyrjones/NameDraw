using System;
using System.Collections.Generic;
using System.Text;

namespace jones.jeremy.namedraw
{
    internal class NameDrawEventResult
    {
        private int eventId = 0;

        internal int EventId { get; private set; }
        internal NameDrawConfig Config { get; set; }
        internal IList<NameDrawResult> Results { get; set; }

        internal NameDrawEventResult()
        {
            EventId = eventId++;
        }
    }
}
