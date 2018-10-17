using System;
using System.Collections.Generic;
using System.Text;

namespace jones.jeremy.namedraw
{
    internal class ExclusionDrawRule : DrawRule
    {
        protected internal ExclusionDrawRule(Person from, Person to) : base(from, to, Rule.Exclude)
        {
        }
    }
}
