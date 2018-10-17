using System;
using System.Collections.Generic;
using System.Text;

namespace jones.jeremy.namedraw
{
    internal class NameDrawConfig
    {
        internal IList<Person> Participants { get; set; } = new List<Person>();
        internal IList<DrawRule> DrawRules { get; set; } = new List<DrawRule>();
        internal IList<NameDrawItem> Items { get; set; } = new List<NameDrawItem>();
        internal DateTime NameDrawDateTime { get; set; }
        internal string NameDrawDescription { get; set; }
        internal IList<INameDrawOutput> OutputProviders { get; set; } = new List<INameDrawOutput>();
    }
}
